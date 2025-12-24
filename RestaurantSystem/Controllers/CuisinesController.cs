using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Models;

namespace RestaurantSystem.Controllers
{
    public class CuisinesController : Controller
    {
        private readonly RestaurantDbContext _context;

        public CuisinesController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: Cuisines
        public async Task<IActionResult> Index()
        {
            return View(await _context.Cuisines.ToListAsync());
        }

        // GET: Cuisines/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisine = await _context.Cuisines
                .FirstOrDefaultAsync(m => m.CuisineId == id);
            if (cuisine == null)
            {
                return NotFound();
            }

            return View(cuisine);
        }

        // GET: Cuisines/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Cuisines/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("CuisineId,Name")] Cuisine cuisine)
        {
            if (ModelState.IsValid)
            {
                _context.Add(cuisine);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(cuisine);
        }

        // GET: Cuisines/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisine = await _context.Cuisines.FindAsync(id);
            if (cuisine == null)
            {
                return NotFound();
            }
            return View(cuisine);
        }

        // POST: Cuisines/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CuisineId,Name")] Cuisine cuisine)
        {
            if (id != cuisine.CuisineId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cuisine);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CuisineExists(cuisine.CuisineId))
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
            return View(cuisine);
        }

        // GET: Cuisines/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cuisine = await _context.Cuisines
                .FirstOrDefaultAsync(m => m.CuisineId == id);
            if (cuisine == null)
            {
                return NotFound();
            }

            return View(cuisine);
        }

        // POST: Cuisines/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cuisine = await _context.Cuisines.FindAsync(id);
            if (cuisine != null)
            {
                _context.Cuisines.Remove(cuisine);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CuisineExists(int id)
        {
            return _context.Cuisines.Any(e => e.CuisineId == id);
        }
    }
}
