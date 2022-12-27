﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemZglaszaniaAwariiGlowny.Data;
using SystemZglaszaniaAwariiGlowny.Models;
using SystemZglaszaniaAwariiGlowny.Models.ModelView;
using Microsoft.AspNetCore.Authorization;
using static System.Net.Mime.MediaTypeNames;

namespace SystemZglaszaniaAwariiGlowny.Controllers
{
    [Authorize(Roles = "admin, pracownik, mechanik")]
    public class MaszynasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MaszynasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Maszynas
        public async Task<IActionResult> Index(int PageNumber = 1)
        {
            MMViewModel mMViewModel = new();
            mMViewModel.MMView = new MMView();

            mMViewModel.MMView.MMCount = _context.Maszynas
            .Where(t => t.Active == true)
            .Count();
            mMViewModel.MMView.PageNumber = PageNumber ;


            mMViewModel.Maszynas = (IEnumerable<Maszyna>?)await _context.Maszynas
                    .Include(t => t.Zgloszenias)
                    .Include(t => t.User)
                    .Where(t => t.Active == true)
                    .Skip((PageNumber - 1) * mMViewModel.MMView.PageSize)
                    .Take(mMViewModel.MMView.PageSize)
                    .ToListAsync();


            return View(mMViewModel);
            // var applicationDbContext = _context.Maszynas
            //       .Include(m => m.Zgloszenias)
            //       .Include(m => m.User)
            //      .Where(m => m.Active == true);
            //    return View(await applicationDbContext.ToListAsync());
        }

        public async Task<IActionResult> List()
        {
            var applicationDbContext = _context.Maszynas.Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Maszynas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Maszynas == null)
            {
                return NotFound();
            }

            var maszyna = await _context.Maszynas
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MaszynaId == id);
            if (maszyna == null)
            {
                return NotFound();
            }

            return View(maszyna);
        }

        // GET: Maszynas/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id");
            return View();
        }

        // POST: Maszynas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaszynaId,MaszynaName,MaszynaOpis,Graphic,Active,Display,Id")] Maszyna maszyna)
        {
            if (ModelState.IsValid)
            {
                if(!MaszynaNameExists(maszyna.MaszynaName))
                {
                    _context.Add(maszyna);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.ErrorMessage = "Maszyna o takiej nazwie już istnieje!";
                    return View("Create");
                }
               
            }
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", maszyna.Id);
            return View(maszyna);
        }

        // GET: Maszynas/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Maszynas == null)
            {
                return NotFound();
            }

            var maszyna = await _context.Maszynas.FindAsync(id);
            if (maszyna == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", maszyna.Id);
            return View(maszyna);
        }

        // POST: Maszynas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MaszynaId,MaszynaName,MaszynaOpis,Graphic,Active,Display,Id")] Maszyna maszyna)
        {
            if (id != maszyna.MaszynaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(maszyna);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MaszynaExists(maszyna.MaszynaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", maszyna.Id);
            return View(maszyna);
        }

        // GET: Maszynas/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Maszynas == null)
            {
                return NotFound();
            }

            var maszyna = await _context.Maszynas
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MaszynaId == id);
            if (maszyna == null)
            {
                return NotFound();
            }

            if (MaszynaZgloszenia(id)) {

                ViewBag.DeleteMessage = "Nie można usunąć wybranej maszyny, ponieważ ma przypisane zgłoszenie";
            
            }

            return View(maszyna);
        }

        // POST: Maszynas/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Maszynas == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Maszynas'  is null.");
            }
            var maszyna = await _context.Maszynas.FindAsync(id);
            if (maszyna != null)
            {
                _context.Maszynas.Remove(maszyna);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MaszynaExists(int id)
        {
          return _context.Maszynas.Any(e => e.MaszynaId == id);
        }

        private bool MaszynaNameExists(string? name)
        {
            return (_context.Maszynas?.Any(e => e.MaszynaName ==
            name)).GetValueOrDefault();
        }
        private bool MaszynaZgloszenia(int id)
        {
            return (_context.Zgloszenias?.Any(t => t.MaszynaId == id)).GetValueOrDefault();

        }

    }
}