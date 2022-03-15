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
    public class StrazacyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StrazacyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Strazacy
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Strazacy.OrderBy(w => w.Nazwisko);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Strazacy/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strazacy = await _context.Strazacy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (strazacy == null)
            {
                return NotFound();
            }

            return View(strazacy);
        }

        // GET: Strazacy/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Strazacy/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Imie,Nazwisko,Data_urodzenia,Data_wstapienia,Kursy,Odznaczenia")] Strazacy strazacy)
        {
            if (ModelState.IsValid)
            {
                _context.Add(strazacy);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(strazacy);
        }

        // GET: Strazacy/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strazacy = await _context.Strazacy.FindAsync(id);
            if (strazacy == null)
            {
                return NotFound();
            }
            return View(strazacy);
        }

        // POST: Strazacy/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Imie,Nazwisko,Data_urodzenia,Data_wstapienia,Kursy,Odznaczenia")] Strazacy strazacy)
        {
            if (id != strazacy.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(strazacy);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StrazacyExists(strazacy.Id))
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
            return View(strazacy);
        }

        // GET: Strazacy/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var strazacy = await _context.Strazacy
                .FirstOrDefaultAsync(m => m.Id == id);
            if (strazacy == null)
            {
                return NotFound();
            }

            return View(strazacy);
        }

        // POST: Strazacy/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var strazacy = await _context.Strazacy.FindAsync(id);
            _context.Strazacy.Remove(strazacy);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StrazacyExists(int id)
        {
            return _context.Strazacy.Any(e => e.Id == id);
        }
    }
}
