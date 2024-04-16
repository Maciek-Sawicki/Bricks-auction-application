using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Items;

namespace Bricks_auction_application.Controllers
{
    public class SetsController : Controller
    {
        private readonly BricksAuctionDbContext _context;

        public SetsController(BricksAuctionDbContext context)
        {
            _context = context;
        }

        // GET: Sets
        public async Task<IActionResult> Index()
        {
            var bricksAuctionDbContext = _context.Sets.Include(s => s.Category);
            return View(await bricksAuctionDbContext.ToListAsync());
        }

        // GET: Sets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // GET: Sets/Create
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName");
            return View();
        }

        // POST: Sets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SetId,Name,Description,ReleaseYear,CategoryId")] Set @set)
        {
            if (ModelState.IsValid)
            {
                _context.Add(@set);
                await _context.SaveChangesAsync();
                TempData["success"] = "Set created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", @set.CategoryId);
            return View(@set);
        }

        // GET: Sets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets.FindAsync(id);
            if (@set == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", @set.CategoryId);
            return View(@set);
        }

        // POST: Sets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SetId,Name,Description,ReleaseYear,CategoryId")] Set @set)
        {
            if (id != @set.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(@set);
                    await _context.SaveChangesAsync();
                    TempData["success"] = "Set updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SetExists(@set.Id))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "CategoryName", @set.CategoryId);
            return View(@set);
        }

        // GET: Sets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var @set = await _context.Sets
                .Include(s => s.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (@set == null)
            {
                return NotFound();
            }

            return View(@set);
        }

        // POST: Sets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var @set = await _context.Sets.FindAsync(id);
            if (@set != null)
            {
                _context.Sets.Remove(@set);
                TempData["success"] = "Set deleted successfully";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SetExists(int id)
        {
            return _context.Sets.Any(e => e.Id == id);
        }
    }
}
