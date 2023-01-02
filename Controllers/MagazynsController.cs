using System;
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
using static System.Net.Mime.MediaTypeNames;
using System.Security.Claims;
using SystemZglaszaniaAwariiGlowny.Infrastruktura;

namespace SystemZglaszaniaAwariiGlowny.Controllers
{
    [Authorize(Roles = "admin, pracownik, mechanik")]
    public class MagazynsController : Controller
    {

        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _hostEnvironment;

        public MagazynsController(ApplicationDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _hostEnvironment = environment;
        }

        // GET: Magazyns
        public async Task<IActionResult> Index(string NazwaMagazynu ,string Fraza,int PageNumber = 1)
                {
            var SelectedTexts = _context.Magazyns?
            .Where(t => t.Active == true);

           
            if (!String.IsNullOrEmpty(Fraza))
            {
                SelectedTexts = (IOrderedQueryable<Magazyn>)SelectedTexts.Where(r => r.MagazynOpis.Contains(Fraza));
            }
            if (!String.IsNullOrEmpty(NazwaMagazynu))
            {
                SelectedTexts = (IOrderedQueryable<Magazyn>)SelectedTexts.Where(r => r.MagazynName.Contains(NazwaMagazynu));
            }





            MGViewModel mMViewModel = new();
            mMViewModel.MMView = new MMView();

            mMViewModel.MMView.MMCount = SelectedTexts.Count();

        //    mMViewModel.MMView.MMCount = _context.Magazyns
        //     .Where(t => t.Active == true)
        //    .Count();
            mMViewModel.MMView.PageNumber = PageNumber;

            mMViewModel.MMView.NazwaMagazynu = NazwaMagazynu;
            mMViewModel.MMView.Fraza = Fraza;


            mMViewModel.Magazyns = (IEnumerable<Magazyn>?)await SelectedTexts
                .Skip((PageNumber - 1) * mMViewModel.MMView.PageSize)
                .Take(mMViewModel.MMView.PageSize)
                    .ToListAsync();

            //    mMViewModel.Magazyns = (IEnumerable<Magazyn>?)await _context.Magazyns
            //       .Include(t => t.Zgloszenias)
            //     .Include(t => t.User)
            //     .Where(t => t.Active == true)
            //     .Skip((PageNumber - 1) * mMViewModel.MMView.PageSize)
            //      .Take(mMViewModel.MMView.PageSize)
            //      .ToListAsync();


            //     ViewData["NazwaMagazynu"] = new SelectList(_context.Magazyns
            //      .Include(u => u.MagazynName)
            //      .Select(u => u.MagazynName)
            //         .Distinct(),
            //        "MagazynId", "MagazynName", NazwaMagazynu);




            return View(mMViewModel);
        }
        [Authorize(Roles = "admin")]
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
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName");
            return View();
        }

        // POST: Magazyns/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("MagazynId,MagazynName,MagazynOpis,Active,Display")] Magazyn magazyn, IFormFile? picture)
        {
            if (ModelState.IsValid)
            {
                magazyn.Id = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (picture != null && picture.Length > 0)
                {
                    ImageFileUpl imageFileResult = new(_hostEnvironment);
                    FileSendRes fileSendResult = imageFileResult.SendFile(picture, "grafika", 300);
                    if (fileSendResult.Success)
                    {
                        magazyn.Graphic = fileSendResult.Name;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Wybrany plik nie jest obrazkiem!";
                        ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", magazyn.MagazynId);
                        return View(magazyn);
                    }
                }

                
                //    if (!MagazynNameExists(magazyn.MagazynName))
                //    {

                       

                        _context.Add(magazyn);
                        await _context.SaveChangesAsync();
                        return RedirectToAction(nameof(List));

              //      }

               //     else
              //      {
                  //      ViewBag.ErrorMessage = "Kategoria o takiej nazwie już istnieje!";
                 //       return View("Create");
               //     }


                

            }
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName",magazyn.MagazynId);
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
            if (string.Compare(User.FindFirstValue(ClaimTypes.NameIdentifier), magazyn.Id) == 0 || User.IsInRole("admin"))
            {
                ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", magazyn.MagazynId);
                ViewData["Id"] = magazyn.Id;
                return View(magazyn);

            }
            else
            {
                return RedirectToAction(nameof(List));
            }

           

            //    ViewData["Id"] = new SelectList(_context.AppUsers, "Id", "Id", magazyn.Id);
            
        }

        // POST: Magazyns/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int magazynid, [Bind("MagazynId,MagazynName,MagazynOpis,Graphic,Active,Display,Id")] Magazyn magazyn, IFormFile? picture)
        {
            if (magazynid != magazyn.MagazynId)
           {
              return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (picture != null && picture.Length > 0)
                {
                    ImageFileUpl imageFileResult = new(_hostEnvironment);
                    FileSendRes fileSendResult = imageFileResult.SendFile(picture, "grafika", 600);
                    if (fileSendResult.Success)
                    {
                        magazyn.Graphic = fileSendResult.Name;
                    }
                    else
                    {
                        ViewBag.ErrorMessage = "Wybrany plik nie jest obrazkiem!";
                        ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", magazyn.MagazynId);
                        ViewData["Id"] = magazyn.Id;
                        return View(magazyn);
                    }
                }

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
                return RedirectToAction(nameof(List));
            }
            ViewData["MagazynId"] = new SelectList(_context.Magazyns, "MagazynId", "MagazynName", magazyn.MagazynId);
            ViewData["Id"] = magazyn.Id;
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


            if (MagazynZgloszenia(id))
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
            return RedirectToAction(nameof(List));
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
        
        private bool MagazynZgloszenia(int id)
        {
            return (_context.Zgloszenias?.Any(t => t.MaszynaId == id)).GetValueOrDefault();

        }


    }









}

