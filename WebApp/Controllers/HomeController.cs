using Microsoft.AspNetCore.Authorization;
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
        [Authorize]
        public async Task<IActionResult> Create()
        {
            Kontakt kontakt = new Kontakt();
            kontakt.DataUrodzenia = DateTime.Now; // Ustaw datę urodzenia na dzisiejszą datę bez godziny

            KontaktCreateViewModel model = new KontaktCreateViewModel
            {
                Kontakt = kontakt,
                WszystkieKategorie = await _context.Kategoria.ToListAsync(),
                WszystkiePodkategorie = await _context.Podkategoria.ToListAsync()
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
                //Sprawdzenie czy email już istniej
                if (IsEmailUnique(kontakt.Email))
                {
                    kontakt.DataUrodzenia = kontakt.DataUrodzenia.Date;
                    _context.Add(kontakt);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                ModelState.AddModelError("Kontakt.Email", "Adres e-mail jest już zajęty.");
            }

            KontaktCreateViewModel model = new KontaktCreateViewModel
            {
                Kontakt = kontakt,
                WszystkieKategorie = await _context.Kategoria.ToListAsync(),
                WszystkiePodkategorie = await _context.Podkategoria.ToListAsync()
            };
            return View(model);
        }

        private bool IsEmailUnique(string email)
        {
            // Sprawdzenie czy istnieją jakiekolwiek kontakty w bazie danych
            if (!_context.Kontakt.Any())
            {
                return true;
            }
            return !_context.Kontakt.Any(x => x.Email == email);
        }

        // GET: Home/Edit/5
        [Authorize]
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
            kontakt.DataUrodzenia = kontakt.DataUrodzenia;
            KontaktCreateViewModel model = new KontaktCreateViewModel
            {
                Kontakt = kontakt,
                WszystkieKategorie = await _context.Kategoria.ToListAsync(),
                WszystkiePodkategorie = await _context.Podkategoria.ToListAsync()
            };

            return View(model);
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
                bool emailTaken = false;

                kontakt.DataUrodzenia = kontakt.DataUrodzenia.Date;
                try
                {
                    if (IsEmailUnique(kontakt.Email))
                    {
                        _context.Update(kontakt);
                        await _context.SaveChangesAsync();
                    }
                    else
                    {
                        emailTaken = true;
                        ModelState.AddModelError("Kontakt.Email", "Adres e-mail jest już zajęty.");
                    }
                        
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
                if (emailTaken == false)
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            KontaktCreateViewModel model = new KontaktCreateViewModel
            {
                Kontakt = kontakt,
                WszystkieKategorie = await _context.Kategoria.ToListAsync(),
                WszystkiePodkategorie = await _context.Podkategoria.ToListAsync()
            };
            return View(model);
        }

        // GET: Home/Delete/5
        [Authorize]
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
        [Authorize]
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