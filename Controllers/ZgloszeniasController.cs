using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AwarieNoweZnowu.Data;
using AwarieNoweZnowu.Models;

namespace AwarieNoweZnowu.Controllers
{
    public class ZgloszeniasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZgloszeniasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zgloszenias
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Zgloszenias.Include(z => z.Magazyn).Include(z => z.Maszyna).Include(z => z.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zgloszenias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zgloszenias == null)
            {
                return NotFound();
            }

            var zgloszenia = await _context.Zgloszenias
                .Include(z => z.Magazyn)
                .Include(z => z.Maszyna)
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.ZgloszeniaId == id);
            if (zgloszenia == null)
            {
                return NotFound();
            }

            return View(zgloszenia);
        }

        // GET: Zgloszenias/Create
        public IActionResult Create()
        {
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName");
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName");
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id");
            return View();
        }

        // POST: Zgloszenias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZgloszeniaId,AwariaOpis,Active,AddedDate,MagazynId,MaszynaId,Id")] Zgloszenia zgloszenia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zgloszenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", zgloszenia.MagazynId);
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", zgloszenia.MaszynaId);
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", zgloszenia.Id);
            return View(zgloszenia);
        }

        // GET: Zgloszenias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Zgloszenias == null)
            {
                return NotFound();
            }

            var zgloszenia = await _context.Zgloszenias.FindAsync(id);
            if (zgloszenia == null)
            {
                return NotFound();
            }
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", zgloszenia.MagazynId);
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", zgloszenia.MaszynaId);
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", zgloszenia.Id);
            return View(zgloszenia);
        }

        // POST: Zgloszenias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZgloszeniaId,AwariaOpis,Active,AddedDate,MagazynId,MaszynaId,Id")] Zgloszenia zgloszenia)
        {
            if (id != zgloszenia.ZgloszeniaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zgloszenia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZgloszeniaExists(zgloszenia.ZgloszeniaId))
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
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", zgloszenia.MagazynId);
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", zgloszenia.MaszynaId);
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", zgloszenia.Id);
            return View(zgloszenia);
        }

        // GET: Zgloszenias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zgloszenias == null)
            {
                return NotFound();
            }

            var zgloszenia = await _context.Zgloszenias
                .Include(z => z.Magazyn)
                .Include(z => z.Maszyna)
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.ZgloszeniaId == id);
            if (zgloszenia == null)
            {
                return NotFound();
            }

            return View(zgloszenia);
        }

        // POST: Zgloszenias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Zgloszenias == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Zgloszenias'  is null.");
            }
            var zgloszenia = await _context.Zgloszenias.FindAsync(id);
            if (zgloszenia != null)
            {
                _context.Zgloszenias.Remove(zgloszenia);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZgloszeniaExists(int id)
        {
          return _context.Zgloszenias.Any(e => e.ZgloszeniaId == id);
        }

       

    }
}
