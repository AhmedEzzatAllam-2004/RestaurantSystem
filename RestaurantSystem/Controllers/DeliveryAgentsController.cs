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
    public class DeliveryAgentsController : Controller
    {
        private readonly RestaurantDbContext _context;

        public DeliveryAgentsController(RestaurantDbContext context)
        {
            _context = context;
        }

        // GET: DeliveryAgents
        public async Task<IActionResult> Index()
        {
            return View(await _context.DeliveryAgents.ToListAsync());
        }

        // GET: DeliveryAgents/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAgent = await _context.DeliveryAgents
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (deliveryAgent == null)
            {
                return NotFound();
            }

            return View(deliveryAgent);
        }

        // GET: DeliveryAgents/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: DeliveryAgents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AgentId,Name,Phone,VehicleType")] DeliveryAgent deliveryAgent)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryAgent);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryAgent);
        }

        // GET: DeliveryAgents/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAgent = await _context.DeliveryAgents.FindAsync(id);
            if (deliveryAgent == null)
            {
                return NotFound();
            }
            return View(deliveryAgent);
        }

        // POST: DeliveryAgents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AgentId,Name,Phone,VehicleType")] DeliveryAgent deliveryAgent)
        {
            if (id != deliveryAgent.AgentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryAgent);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryAgentExists(deliveryAgent.AgentId))
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
            return View(deliveryAgent);
        }

        // GET: DeliveryAgents/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deliveryAgent = await _context.DeliveryAgents
                .FirstOrDefaultAsync(m => m.AgentId == id);
            if (deliveryAgent == null)
            {
                return NotFound();
            }

            return View(deliveryAgent);
        }

        // POST: DeliveryAgents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deliveryAgent = await _context.DeliveryAgents.FindAsync(id);
            if (deliveryAgent != null)
            {
                _context.DeliveryAgents.Remove(deliveryAgent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryAgentExists(int id)
        {
            return _context.DeliveryAgents.Any(e => e.AgentId == id);
        }
    }
}
