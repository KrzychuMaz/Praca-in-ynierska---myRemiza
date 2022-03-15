using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using myRemiza.Data;
using myRemiza.Models;

namespace myRemiza.Controllers
{
    [Authorize(Roles = "Basic")]
    public class SprzetController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SprzetController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Sprzet
        public async Task<IActionResult> Index(string searchString)
        {
            var applicationDbContext = _context.Sprzet.Include(s => s.Magazyn).Include(w => w.Samochody).OrderBy(m=>m.Data_Przegladu);
            if (!String.IsNullOrEmpty(searchString))
            {
                applicationDbContext = (IOrderedQueryable<Sprzet>)applicationDbContext.Where(s => s.Firma!.Contains(searchString) || s.Rodzaj!.Contains(searchString) || s.Nazwa!.Contains(searchString));
            }
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Sprzet/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprzet = await _context.Sprzet
                .Include(s => s.Magazyn)
                .Include(s => s.Samochody)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sprzet == null)
            {
                return NotFound();
            }

            return View(sprzet);
        }

        // GET: Sprzet/Create
        public IActionResult Create()
        {
            ViewBag.Samochody = _context.Samochody.ToDictionary(u => u.Id, u => u.Typ);
            ViewBag.Magazyn = _context.Magazyn.ToDictionary(u => u.Id, u => u.Nazwa);

            return View();
        }
        // POST: Sprzet/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa,Firma,Rodzaj,Nr_seryjny,MagazynId,Data_pozyskania,Data_Przegladu")] Sprzet sprzet)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sprzet);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MagazynId"] = new SelectList(_context.Magazyn, "Id", "Id", sprzet.MagazynId);
            ViewData["MagazynId"] = new SelectList(_context.Samochody, "Id", "Id", sprzet.MagazynId);
            return View(sprzet);
        }

        // GET: Sprzet/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprzet = await _context.Sprzet.FindAsync(id);
            if (sprzet == null)
            {
                return NotFound();
            }
            ViewBag.Samochody = _context.Samochody.ToDictionary(u => u.Id, u => u.Typ);
            ViewBag.Magazyn = _context.Magazyn.ToDictionary(u => u.Id, u => u.Nazwa);
            return View(sprzet);
        }

        // POST: Sprzet/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa,Firma,Rodzaj,Nr_seryjny,MagazynId,Data_pozyskania,Data_Przegladu")] Sprzet sprzet)
        {
            if (id != sprzet.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sprzet);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SprzetExists(sprzet.Id))
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
            ViewData["MagazynId"] = new SelectList(_context.Magazyn, "Id", "Id", sprzet.MagazynId);
            ViewData["MagazynId"] = new SelectList(_context.Samochody, "Id", "Id", sprzet.MagazynId);
            return View(sprzet);
        }

        // GET: Sprzet/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var sprzet = await _context.Sprzet
                .Include(s => s.Magazyn)
                .Include(s => s.Samochody)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (sprzet == null)
            {
                return NotFound();
            }

            return View(sprzet);
        }

        // POST: Sprzet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var sprzet = await _context.Sprzet.FindAsync(id);
            _context.Sprzet.Remove(sprzet);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SprzetExists(int id)
        {
            return _context.Sprzet.Any(e => e.Id == id);
        }
    }
}
