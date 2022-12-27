﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemZglaszaniaAwariiGlowny.Data;
using SystemZglaszaniaAwariiGlowny.Models;
using Microsoft.AspNetCore.Authorization;
using SystemZglaszaniaAwariiGlowny.Models.ModelView;

namespace SystemZglaszaniaAwariiGlowny.Controllers
{
    [Authorize(Roles = "admin, pracownik, mechanik")]
    public class MagazynsController : Controller
    {

        private readonly ApplicationDbContext _context;

        public MagazynsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Magazyns
        public async Task<IActionResult> Index(string NazwaMagazynu ,string OpisMagazynu, string Autor,int PageNumber = 1)
        {

           



            MGViewModel mMViewModel = new();
            mMViewModel.MMView = new MMView();

            mMViewModel.MMView.MMCount = _context.Magazyns
            .Where(t => t.Active == true)
            .Count();
            mMViewModel.MMView.PageNumber = PageNumber;

            mMViewModel.MMView.NazwaMagazynu = Autor;
            mMViewModel.MMView.OpisMagazynu = OpisMagazynu;
            mMViewModel.MMView.Autor = Autor;

            mMViewModel.Magazyns = (IEnumerable<Magazyn>?)await _context.Magazyns
                    .Include(t => t.Zgloszenias)
                    .Include(t => t.User)
                    .Where(t => t.Active == true)
                    .Skip((PageNumber - 1) * mMViewModel.MMView.PageSize)
                    .Take(mMViewModel.MMView.PageSize)
                    .ToListAsync();

            ViewData["Autor"] = new SelectList(_context.Magazyns
            .Include(u => u.User)
            .Select(u => u.User)
            .Distinct(),
            "Id", "FullName", Autor);

            ViewData["NazwaMagazynu"] = new SelectList(_context.Magazyns
            .Include(u => u.MagazynName)
            .Select(u => u.MagazynName)
            .Distinct(),
            "MagazynId", "MagazynName", NazwaMagazynu);

            ViewData["OpisMagazynu"] = new SelectList(_context.Magazyns
            .Include(u => u.MagazynOpis)
            .Select(u => u.MagazynOpis)
            .Distinct(),
            "MagazynId", "MagazynOpis", OpisMagazynu);



            return View(mMViewModel);
        }
        public async Task<IActionResult> List()
        {
            var applicationDbContext = _context.Magazyns.Include(m => m.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Magazyns/Details/5


        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Magazyns == null)
            {
                return NotFound();
            }

            var magazyn = await _context.Magazyns
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MagazynId == id);
            if (magazyn == null)
            {
                return NotFound();
            }

            return View(magazyn);
        }

        // GET: Magazyns/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id");
            return View();
        }

        // POST: Magazyns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("MagazynId,MagazynName,MagazynOpis,Graphic,Active,Display,Id")] Magazyn magazyn)
        {
            if (ModelState.IsValid)
            {
                {
                    if (!MagazynNameExists(magazyn.MagazynName))
                    {
                        _context.Add(magazyn);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));

                    }

                    else
                    {
                        ViewBag.ErrorMessage = "Kategoria o takiej nazwie już istnieje!";
                        return View("Create");
                    }



                }

            }
            return View(magazyn);
        }

        // GET: Magazyns/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Magazyns == null)
            {
                return NotFound();
            }

            var magazyn = await _context.Magazyns.FindAsync(id);
            if (magazyn == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", magazyn.Id);
            return View(magazyn);
        }

        // POST: Magazyns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MagazynId,MagazynName,MagazynOpis,Graphic,Active,Display,Id")] Magazyn magazyn)
        {
            if (id != magazyn.MagazynId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(magazyn);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MagazynExists(magazyn.MagazynId))
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
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", magazyn.Id);
            return View(magazyn);
        }

        // GET: Magazyns/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _context.Magazyns == null)
            {
                return NotFound();
            }

            var magazyn = await _context.Magazyns
                .Include(m => m.User)
                .FirstOrDefaultAsync(m => m.MagazynId == id);
            if (magazyn == null)
            {
                return NotFound();
            }


            if (TextsInCategory(id))
            {
                ViewBag.DeleteMessage = "Nie można usunąć wybranego Magazynu, gdyż posiada przypisane zgłoszenia.";
            }
            return View(magazyn);
        }

        // POST: Magazyns/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Magazyns == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Magazyns'  is null.");
            }
            var magazyn = await _context.Magazyns.FindAsync(id);
            if (magazyn != null)
            {
                _context.Magazyns.Remove(magazyn);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MagazynExists(int id)
        {
            return _context.Magazyns.Any(e => e.MagazynId == id);



        }

        private bool MagazynNameExists(string? name)
        {
            return (_context.Magazyns?.Any(e => e.MagazynName ==
            name)).GetValueOrDefault();
        }



        private bool TextsInCategory(int id)
        {
            return (_context.Magazyns?.Any(t => t.MagazynId == id)).GetValueOrDefault();
        }


    }









}
