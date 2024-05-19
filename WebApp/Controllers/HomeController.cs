using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WebApp.Data;
using WebApp.Models;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return _context.Kontakt != null ?
                        View(await _context.Kontakt.ToListAsync()) :
                        Problem("Entity set 'ApplicationDbContext.Kontakt'  is null.");
        }

        // GET: Home/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Kontakt == null)
            {
                return NotFound();
            }

            var kontakt = await _context.Kontakt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kontakt == null)
            {
                return NotFound();
            }

            return View(kontakt);
        }

        // GET: Home/Create
        public async Task<IActionResult> Create()
        {
            Kontakt kontakt = new Kontakt();
            kontakt.DataUrodzenia = DateTime.Now; // Ustaw datę urodzenia na dzisiejszą datę bez godziny

            KontaktCreateViewModel model = new KontaktCreateViewModel
            {
                Kontakt = kontakt,
                WszystkieKategorie = await _context.Kategoria.ToListAsync()
            };

            return View(model);
        }

        // POST: Home/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Email,Haslo,Kategoria,Telefon,DataUrodzenia")] Kontakt kontakt)
        {
            if (ModelState.IsValid)
            {
                kontakt.DataUrodzenia = kontakt.DataUrodzenia.Date;
                _context.Add(kontakt);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kontakt);
        }

        // GET: Home/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Kontakt == null)
            {
                return NotFound();
            }

            var kontakt = await _context.Kontakt.FindAsync(id);
            if (kontakt == null)
            {
                return NotFound();
            }
            return View(kontakt);
        }

        // POST: Home/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Email,Haslo,Kategoria,Telefon,DataUrodzenia")] Kontakt kontakt)
        {
            if (id != kontakt.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kontakt);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KontaktExists(kontakt.Id))
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
            return View(kontakt);
        }

        // GET: Home/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Kontakt == null)
            {
                return NotFound();
            }

            var kontakt = await _context.Kontakt
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kontakt == null)
            {
                return NotFound();
            }

            return View(kontakt);
        }

        // POST: Home/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Kontakt == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Kontakt'  is null.");
            }
            var kontakt = await _context.Kontakt.FindAsync(id);
            if (kontakt != null)
            {
                _context.Kontakt.Remove(kontakt);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KontaktExists(int id)
        {
            return (_context.Kontakt?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}