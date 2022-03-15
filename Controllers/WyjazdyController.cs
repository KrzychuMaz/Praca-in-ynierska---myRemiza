using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myRemiza.Data;
using myRemiza.Enums;
using myRemiza.Models;

namespace myRemiza.Controllers
{
    [Authorize(Roles = "Basic")]
    public class WyjazdyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WyjazdyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Wyjazdy
        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = _context.Wyjazdy.Include(w => w.Rodzaj_zdarzenia).Include(w => w.Samochody).OrderByDescending(w => w.Data);
            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = (IOrderedQueryable<Wyjazdy>)applicationDbContext.Where(s => s.Opis!.Contains(searchString)|| s.Miejsce!.Contains(searchString) || s.Strazacy!.Contains(searchString));
                
            }
            return View(await applicationDbContext.ToListAsync());
        }
        [HttpPost]
        public string Index(string searchString, bool notUsed)
        {
            return "From [HttpPost]Index: filter on " + searchString;
        }
        // GET: Wyjazdy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wyjazdy = await _context.Wyjazdy
                .Include(w => w.Rodzaj_zdarzenia)
                .Include(w => w.Samochody)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wyjazdy == null)
            {
                return NotFound();
            }

            return View(wyjazdy);
        }

        // GET: Wyjazdy/Create
        public IActionResult Create()
        {
            ViewBag.Rodzaj_zdarzenia = _context.Rodzaj_zdarzenia.ToDictionary(u => u.Id, u => u.Nazwa);
            ViewBag.Samochody = _context.Samochody.ToDictionary(u => u.Id, u => u.Typ);
            return View();
        }

        // POST: Wyjazdy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Data,Miejsce,Rodzaj_zdarzeniaId,Strazacy,SamochodyId,SamochodyId2,Opis")] Wyjazdy wyjazdy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(wyjazdy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Rodzaj_zdarzeniaId"] = new SelectList(_context.Rodzaj_zdarzenia, "Id", "Id", wyjazdy.Rodzaj_zdarzeniaId);
            ViewData["SamochodyId"] = new SelectList(_context.Samochody, "Id", "Id", wyjazdy.SamochodyId);
            ViewData["SamochodyId2"] = new SelectList(_context.Samochody, "Id", "Id", wyjazdy.SamochodyId);
            return View(wyjazdy);
        }

        // GET: Wyjazdy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wyjazdy = await _context.Wyjazdy.FindAsync(id);
            if (wyjazdy == null)
            {
                return NotFound();
            }
            ViewBag.Rodzaj_zdarzenia = _context.Rodzaj_zdarzenia.ToDictionary(u => u.Id, u => u.Nazwa);
            ViewBag.Samochody = _context.Samochody.ToDictionary(u => u.Id, u => u.Typ);
            return View(wyjazdy);
        }

        // POST: Wyjazdy/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Data,Miejsce,Rodzaj_zdarzeniaId,Strazacy,SamochodyId,SamochodyId2,Opis")] Wyjazdy wyjazdy)
        {
            if (id != wyjazdy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(wyjazdy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WyjazdyExists(wyjazdy.Id))
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
            ViewData["Rodzaj_zdarzeniaId"] = new SelectList(_context.Rodzaj_zdarzenia, "Id", "Id", wyjazdy.Rodzaj_zdarzeniaId);
            ViewData["SamochodyId"] = new SelectList(_context.Samochody, "Id", "Id", wyjazdy.SamochodyId);
            ViewData["SamochodyId2"] = new SelectList(_context.Samochody, "Id", "Id", wyjazdy.SamochodyId);
            return View(wyjazdy);
        }

        // GET: Wyjazdy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var wyjazdy = await _context.Wyjazdy
                .Include(w => w.Rodzaj_zdarzenia)
                .Include(w => w.Samochody)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (wyjazdy == null)
            {
                return NotFound();
            }

            return View(wyjazdy);
        }

        // POST: Wyjazdy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var wyjazdy = await _context.Wyjazdy.FindAsync(id);
            _context.Wyjazdy.Remove(wyjazdy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WyjazdyExists(int id)
        {
            return _context.Wyjazdy.Any(e => e.Id == id);
        }
    }
}
