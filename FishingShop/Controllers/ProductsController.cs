using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FishingShop;
using FishingShop.Models;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace FishingShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ShopDatabaseContext _context;

        public ProductsController(ShopDatabaseContext context)
        {
            _context = context;
        }
        // GET: Products
        public async Task<IActionResult> Index(string SearchString, int?categoryID)
        {
            ViewData["Category"] = new SelectList(_context.Categories, "IdCategory", "Name");
            var shopDatabaseContext = from p in _context.Products
                                      .Include(p => p.CategoryNavigation)
                                      select p;
                                      // _context.Products.Include(p => p.CategoryNavigation);
            if (categoryID.HasValue && SearchString.IsNullOrEmpty())
            {
                shopDatabaseContext = shopDatabaseContext.Where(p => p.Category == categoryID);
            }
            if (!SearchString.IsNullOrEmpty())
            {
                shopDatabaseContext = shopDatabaseContext.Where(p => p.Name.ToUpper().Contains(SearchString.ToUpper()));
            }
            return View(await shopDatabaseContext.ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.Reviews = new List<Review>(_context.Reviews.Where(p => p.IdProduct == id));
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }
        // POST: Products/Details
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Details")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DetailsAdd(int id, string reviewText, int rating)
        {
			ViewBag.Reviews = new List<Review>(_context.Reviews.Where(p => p.IdProduct == id));
			Review review = new Review
			{
				IdProduct = id,
				Text = reviewText,
				Rating = rating
			};
			Console.WriteLine($"ReviewId = {review.IdReview} productid = {id} reviewtext = {reviewText} rating = {rating}");
			_context.Add(review);
			await _context.SaveChangesAsync();
			var product = await _context.Products
				.Include(p => p.CategoryNavigation)
				.FirstOrDefaultAsync(m => m.IdProduct == id);
			return View(product);
        }

        // GET: Products/Create
        [Authorize(Roles = "admin")]
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(_context.Categories, "IdCategory", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProduct,Image,Price,Description,Category,Name")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                Console.WriteLine($"{product.Name}, {product.Category}, {product.Price}, {product.Description}, {product.Image}");
            }
            ViewData["Category"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", product.Category);
            return View(product);
        }

        // GET: Products/Edit/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Category"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", product.Category);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdProduct,Image,Price,Description,Category,Name")] Product product)
        {
            if (id != product.IdProduct)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.IdProduct))
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
            ViewData["Category"] = new SelectList(_context.Categories, "IdCategory", "IdCategory", product.Category);
            return View(product);
        }

        // GET: Products/Delete/5
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.CategoryNavigation)
                .FirstOrDefaultAsync(m => m.IdProduct == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [Authorize(Roles = "admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'ShopDatabaseContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.IdProduct == id)).GetValueOrDefault();
        }
    }
}
