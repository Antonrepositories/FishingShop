using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FishingShop;
using FishingShop.Models;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FishingShop.Controllers
{
    public class DeliveryPointsController : Controller
    {
        private readonly ShopDatabaseContext _context;

        public DeliveryPointsController(ShopDatabaseContext context)
        {
            _context = context;
        }

		// GET: DeliveryPoints
		public async Task<IActionResult> Index()
        {
              return _context.DeliveryPoints != null ? 
                          View(await _context.DeliveryPoints.ToListAsync()) :
                          Problem("Entity set 'ShopDatabaseContext.DeliveryPoints'  is null.");
        }

		// GET: DeliveryPoints/Details/5
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.DeliveryPoints == null)
            {
                return NotFound();
            }

            var deliveryPoint = await _context.DeliveryPoints
                .FirstOrDefaultAsync(m => m.IdPoint == id);
            if (deliveryPoint == null)
            {
                return NotFound();
            }

            return View(deliveryPoint);
        }

		// GET: DeliveryPoints/Create
		[Authorize(Roles = "admin")]
		public IActionResult Create()
        {
            return View();
        }

		// POST: DeliveryPoints/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdPoint,Image,Adress")] DeliveryPoint deliveryPoint)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deliveryPoint);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryPoint);
        }

		// GET: DeliveryPoints/Edit/5
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.DeliveryPoints == null)
            {
                return NotFound();
            }

            var deliveryPoint = await _context.DeliveryPoints.FindAsync(id);
            if (deliveryPoint == null)
            {
                return NotFound();
            }
            return View(deliveryPoint);
        }

		// POST: DeliveryPoints/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to.
		// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "admin")]
		[HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdPoint,Image,Adress")] DeliveryPoint deliveryPoint)
        {
            if (id != deliveryPoint.IdPoint)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deliveryPoint);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeliveryPointExists(deliveryPoint.IdPoint))
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
            return View(deliveryPoint);
        }

		// GET: DeliveryPoints/Delete/5
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.DeliveryPoints == null)
            {
                return NotFound();
            }

            var deliveryPoint = await _context.DeliveryPoints
                .FirstOrDefaultAsync(m => m.IdPoint == id);
            if (deliveryPoint == null)
            {
                return NotFound();
            }

            return View(deliveryPoint);
        }

		// POST: DeliveryPoints/Delete/5
		[Authorize(Roles = "admin")]
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.DeliveryPoints == null)
            {
                return Problem("Entity set 'ShopDatabaseContext.DeliveryPoints'  is null.");
            }
            var deliveryPoint = await _context.DeliveryPoints.FindAsync(id);
            if (deliveryPoint != null)
            {
                _context.DeliveryPoints.Remove(deliveryPoint);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeliveryPointExists(int id)
        {
          return (_context.DeliveryPoints?.Any(e => e.IdPoint == id)).GetValueOrDefault();
        }
    }
}
