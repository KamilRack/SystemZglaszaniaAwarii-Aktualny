using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SystemZglaszaniaAwariiGlowny.Data;
using SystemZglaszaniaAwariiGlowny.Models;

namespace SystemZglaszaniaAwariiGlowny.Controllers
{
    public class ZgloszeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZgloszeniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zgloszenia
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Zgloszenias.Include(z => z.Magazyn).Include(z => z.Maszyna).Include(z => z.Mechanik).Include(z => z.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Zgloszenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Zgloszenias == null)
            {
                return NotFound();
            }

            var zgloszenia = await _context.Zgloszenias
                .Include(z => z.Magazyn)
                .Include(z => z.Maszyna)
                .Include(z => z.Mechanik)
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.ZgloszeniaId == id);
            if (zgloszenia == null)
            {
                return NotFound();
            }

            return View(zgloszenia);
        }

        // GET: Zgloszenia/Create
        public IActionResult Create()
        {
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName");
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName");
            ViewData["MechanikId"] = new SelectList(_context.Mechaniks, "MechanikId", "MechanikName");
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id");
            return View();
        }

        // POST: Zgloszenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZgloszeniaId,AwariaOpis,Active,AddedDate,MagazynId,MaszynaId,Id,MechanikId")] Zgloszenia zgloszenia)
        {
            if (ModelState.IsValid)
            {
                zgloszenia.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                zgloszenia.AddedDate = DateTime.Now;
                zgloszenia.MechanikId = 1;
                zgloszenia.Active = true;
                _context.Add(zgloszenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", zgloszenia.MagazynId);
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", zgloszenia.MaszynaId);
            ViewData["MechanikId"] = new SelectList(_context.Mechaniks, "MechanikId", "MechanikName", zgloszenia.MechanikId);
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", zgloszenia.Id);
            return View(zgloszenia);
        }

        // GET: Zgloszenia/Edit/5
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
            ViewData["MechanikId"] = new SelectList(_context.Mechaniks, "MechanikId", "MechanikName", zgloszenia.MechanikId);
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", zgloszenia.Id);
            return View(zgloszenia);
        }

        // POST: Zgloszenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZgloszeniaId,AwariaOpis,Active,AddedDate,MagazynId,MaszynaId,Id,MechanikId")] Zgloszenia zgloszenia)
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
            ViewData["MechanikId"] = new SelectList(_context.Mechaniks, "MechanikId", "MechanikName", zgloszenia.MechanikId);
            ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", zgloszenia.Id);
            return View(zgloszenia);
        }

        // GET: Zgloszenia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Zgloszenias == null)
            {
                return NotFound();
            }

            var zgloszenia = await _context.Zgloszenias
                .Include(z => z.Magazyn)
                .Include(z => z.Maszyna)
                .Include(z => z.Mechanik)
                .Include(z => z.User)
                .FirstOrDefaultAsync(m => m.ZgloszeniaId == id);
            if (zgloszenia == null)
            {
                return NotFound();
            }

            return View(zgloszenia);
        }

        // POST: Zgloszenia/Delete/5
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
