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
    [Authorize(Roles = "SuperAdmin")]
    public class Rodzaj_zdarzeniaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public Rodzaj_zdarzeniaController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Rodzaj_zdarzenia
        public async Task<IActionResult> Index()
        {
            return View(await _context.Rodzaj_zdarzenia.ToListAsync());
        }

        // GET: Rodzaj_zdarzenia/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj_zdarzenia = await _context.Rodzaj_zdarzenia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzaj_zdarzenia == null)
            {
                return NotFound();
            }

            return View(rodzaj_zdarzenia);
        }

        // GET: Rodzaj_zdarzenia/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Rodzaj_zdarzenia/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nazwa")] Rodzaj_zdarzenia rodzaj_zdarzenia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(rodzaj_zdarzenia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(rodzaj_zdarzenia);
        }

        // GET: Rodzaj_zdarzenia/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj_zdarzenia = await _context.Rodzaj_zdarzenia.FindAsync(id);
            if (rodzaj_zdarzenia == null)
            {
                return NotFound();
            }
            return View(rodzaj_zdarzenia);
        }

        // POST: Rodzaj_zdarzenia/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nazwa")] Rodzaj_zdarzenia rodzaj_zdarzenia)
        {
            if (id != rodzaj_zdarzenia.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(rodzaj_zdarzenia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!Rodzaj_zdarzeniaExists(rodzaj_zdarzenia.Id))
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
            return View(rodzaj_zdarzenia);
        }

        // GET: Rodzaj_zdarzenia/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var rodzaj_zdarzenia = await _context.Rodzaj_zdarzenia
                .FirstOrDefaultAsync(m => m.Id == id);
            if (rodzaj_zdarzenia == null)
            {
                return NotFound();
            }

            return View(rodzaj_zdarzenia);
        }

        // POST: Rodzaj_zdarzenia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var rodzaj_zdarzenia = await _context.Rodzaj_zdarzenia.FindAsync(id);
            _context.Rodzaj_zdarzenia.Remove(rodzaj_zdarzenia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool Rodzaj_zdarzeniaExists(int id)
        {
            return _context.Rodzaj_zdarzenia.Any(e => e.Id == id);
        }
    }
}
