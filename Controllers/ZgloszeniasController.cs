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
using SystemZglaszaniaAwariiGlowny.Models.ModelView;
using static System.Net.Mime.MediaTypeNames;

namespace SystemZglaszaniaAwariiGlowny.Controllers
{
    public class ZgloszeniasController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ZgloszeniasController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Zgloszenias
        public async Task<IActionResult> Index(int PageNumber = 1)
        {
            //var applicationDbContext = _context.Zgloszenias
            //    .Include(z => z.Magazyn)
            //    .Include(z => z.Maszyna)
            //    .Include(z => z.Mechanik)
            //    .Include(z => z.User)
            //    .Where(z => z.Active == true)
            //    .OrderByDescending(z => z.AddedDate);

            ZgloszeniaViewModel zgloszeniaViewModel = new();
            zgloszeniaViewModel.MMView = new MMView();

            zgloszeniaViewModel.MMView.MMCount = _context.Zgloszenias
            .Where(t => t.Active == true)
            .Count();

            zgloszeniaViewModel.MMView.PageNumber = PageNumber;

            zgloszeniaViewModel.Zgloszenias = (IEnumerable<Zgloszenia>?)await _context.Zgloszenias
            .Include(t => t.Magazyn)
            .Include(t => t.Maszyna)
            .Include(t => t.Mechanik)
            .Include(t => t.User)
            .Where(t => t.Active == true)
            .OrderByDescending(t => t.AddedDate)
            .Skip((PageNumber - 1) * zgloszeniaViewModel.MMView.PageSize)
            .Take(zgloszeniaViewModel.MMView.PageSize)
            .ToListAsync();


            return View(zgloszeniaViewModel);

        }
        public async Task<IActionResult> List()
        {
            var applicationDbContext = _context.Zgloszenias.Include(z => z.Magazyn).Include(z => z.Maszyna).Include(z => z.Mechanik).Include(z => z.User);
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
                .Include(z => z.Mechanik)
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
              ViewData["MechanikId"] = new SelectList(_context.Mechaniks, "MechanikId", "MechanikName");
              ViewData["ZgloszeniaId"] = new SelectList(_context.Zgloszenias, "ZgloszeniaId", "ZgloszeniaId");
           
            return View();
        }

        // POST: Zgloszenias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //  public async Task<IActionResult> Create([Bind("ZgloszeniaId,AwariaOpis,Active,MagazynId,MaszynaId,MechanikId")] Zgloszenia zgloszenia)
        public async Task<IActionResult> Create([Bind("ZgloszeniaId,AwariaOpis,Active,MagazynId,MaszynaId,MechanikId")] Zgloszenia zgloszenia)
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
            ViewData["ZgloszeniaId"] = new SelectList(_context.Zgloszenias, "ZgloszeniaId", "ZgloszeniaId", zgloszenia.ZgloszeniaId);
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
            if (string.Compare(User.FindFirstValue(ClaimTypes.NameIdentifier), zgloszenia.Id) == 0 || User.IsInRole("admin") || User.IsInRole("mechanik"))
            {
                  ViewData["ZgloszeniaId"] = new SelectList(_context.Zgloszenias, "ZgloszeniaId", "ZgloszeniaId", zgloszenia.ZgloszeniaId);
                  ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", zgloszenia.MagazynId);
                  ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", zgloszenia.MaszynaId);
                  ViewData["MechanikId"] = new SelectList(_context.Mechaniks, "MechanikId", "MechanikName", zgloszenia.MechanikId);
                  ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", zgloszenia.Id);
            }
            else
            {
                return RedirectToAction(nameof(Index));
            }

          
            return View(zgloszenia);
        }

        // POST: Zgloszenias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int zgloszeniaid, [Bind("ZgloszeniaId,AwariaOpis,Active,AddedDate,MagazynId,MaszynaId,Id,MechanikId")] Zgloszenia zgloszenia)
        {
            if (zgloszeniaid != zgloszenia.ZgloszeniaId)
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
            ViewData["ZgloszeniaId"] = new SelectList(_context.Zgloszenias, "ZgloszeniaId", "ZgloszeniaId", zgloszenia.ZgloszeniaId);
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", zgloszenia.MagazynId);
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", zgloszenia.MaszynaId);
            ViewData["MechanikId"] = new SelectList(_context.Mechaniks, "MechanikId", "MechanikName", zgloszenia.MechanikId);
            //ViewData["Id"] = zgloszenia.Id;
            ViewData["Id"] = zgloszenia.Id;
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
                .Include(z => z.Mechanik)
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
