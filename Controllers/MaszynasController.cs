using System;
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
using Microsoft.Extensions.Hosting;
using System.Security.Claims;
using SystemZglaszaniaAwariiGlowny.Infrastruktura;

namespace SystemZglaszaniaAwariiGlowny.Controllers
{
    [Authorize(Roles = "admin, pracownik, mechanik")]
    public class MaszynasController : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _hostEnvironment;

        public MaszynasController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        // GET: Maszynas
        public async Task<IActionResult> Index(string NazwaMaszyny, string FrazaM, int PageNumber = 1)
        {

          var SelectedTexts = _context.Maszynas?
         .Where(t => t.Active == true);


            if (!String.IsNullOrEmpty(FrazaM))
            {
                SelectedTexts = (IOrderedQueryable<Maszyna>)SelectedTexts.Where(r => r.MaszynaOpis.Contains(FrazaM));
            }
            if (!String.IsNullOrEmpty(NazwaMaszyny))
            {
                SelectedTexts = (IOrderedQueryable<Maszyna>)SelectedTexts.Where(r => r.MaszynaName.Contains(NazwaMaszyny));
            }

            MMViewModel mMViewModel = new();
            mMViewModel.MMView = new MMView();

            mMViewModel.MMView.MMCount = SelectedTexts.Count();
        
            mMViewModel.MMView.PageNumber = PageNumber ;

            mMViewModel.MMView.NazwaMaszyny = NazwaMaszyny;
            mMViewModel.MMView.FrazaM = FrazaM;

            mMViewModel.Maszynas = (IEnumerable<Maszyna>?)await SelectedTexts
            .Skip((PageNumber - 1) * mMViewModel.MMView.PageSize)
            .Take(mMViewModel.MMView.PageSize)
                .ToListAsync();
      


            return View(mMViewModel);
            
        }
        [Authorize(Roles = "admin")]
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
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName");
            return View();
        }

        // POST: Maszynas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MaszynaId,MaszynaName,MaszynaOpis,Active,Display")] Maszyna maszyna, IFormFile? picture)
        {
            if (ModelState.IsValid)
            {
                maszyna.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (picture != null && picture.Length > 0)
                {
                    ImageFileUplM imageFileResult = new(_hostEnvironment);
                    FileSendRes fileSendResult = imageFileResult.SendFile(picture, "grafika", 300);
                    if (fileSendResult.Success)
                    {
                        maszyna.Graphic = fileSendResult.Name;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Wybrany plik nie jest obrazkiem!";
                        ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaId", maszyna.MaszynaId);
                        return View(maszyna);
                    }
                }





                _context.Add(maszyna);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(List));

               




            }
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", maszyna.MaszynaId);
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
            if (string.Compare(User.FindFirstValue(ClaimTypes.NameIdentifier), maszyna.Id) == 0 || User.IsInRole("admin"))
            {
                ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", maszyna.MaszynaId);
                ViewData["Id"] = maszyna.Id;
                return View(maszyna);

            }
            else
            {
                return RedirectToAction(nameof(List));
            }
        }

        // POST: Maszynas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int maszynaid, [Bind("MaszynaId,MaszynaName,MaszynaOpis,Graphic,Active,Display,Id")] Maszyna maszyna, IFormFile? picture)
        {
            if (maszynaid != maszyna.MaszynaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (picture != null && picture.Length > 0)
                {
                    ImageFileUplM imageFileResult = new(_hostEnvironment);
                    FileSendRes fileSendResult = imageFileResult.SendFile(picture, "grafika", 600);
                    if (fileSendResult.Success)
                    {
                        maszyna.Graphic = fileSendResult.Name;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Wybrany plik nie jest obrazkiem!";
                        ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", maszyna.MaszynaId);
                        ViewData["Id"] = maszyna.Id;
                        return View(maszyna);
                    }
                }

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
                return RedirectToAction(nameof(List));
            }
            ViewData["MaszynaId"] = new SelectList(_context.Maszynas, "MaszynaId", "MaszynaName", maszyna.MaszynaId);
            ViewData["Id"] = maszyna.Id;
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
            return RedirectToAction(nameof(List));
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
