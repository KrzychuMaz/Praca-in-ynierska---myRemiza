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
    public class SamochodyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SamochodyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Samochody
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Samochody.OrderByDescending(w => w.Data_pozyskania);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Samochody/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samochody = await _context.Samochody
                .FirstOrDefaultAsync(m => m.Id == id);
            if (samochody == null)
            {
                return NotFound();
            }

            return View(samochody);
        }

        // GET: Samochody/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Samochody/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Marka,Model,Typ,Oznaczenie,Nr_rejestracyjny,Data_pozyskania,Przeglad")] Samochody samochody)
        {
            if (ModelState.IsValid)
            {
                _context.Add(samochody);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(samochody);
        }

        // GET: Samochody/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samochody = await _context.Samochody.FindAsync(id);
            if (samochody == null)
            {
                return NotFound();
            }
            return View(samochody);
        }

        // POST: Samochody/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Marka,Model,Typ,Oznaczenie,Nr_rejestracyjny,Data_pozyskania,Przeglad")] Samochody samochody)
        {
            if (id != samochody.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(samochody);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SamochodyExists(samochody.Id))
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
            return View(samochody);
        }

        // GET: Samochody/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var samochody = await _context.Samochody
                .FirstOrDefaultAsync(m => m.Id == id);
            if (samochody == null)
            {
                return NotFound();
            }

            return View(samochody);
        }

        // POST: Samochody/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var samochody = await _context.Samochody.FindAsync(id);
            _context.Samochody.Remove(samochody);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SamochodyExists(int id)
        {
            return _context.Samochody.Any(e => e.Id == id);
        }
    }
}
