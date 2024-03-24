using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Users;

namespace Bricks_auction_application.Controllers
{
    public class OrderedCartsController : Controller
    {
        private readonly BricksAuctionDbContext _context;

        public OrderedCartsController(BricksAuctionDbContext context)
        {
            _context = context;
        }

        // GET: OrderedCarts
        public async Task<IActionResult> Index()
        {
            var bricksAuctionDbContext = _context.OrderedCarts.Include(o => o.OrdersHistory);
            return View(await bricksAuctionDbContext.ToListAsync());
        }

        // GET: OrderedCarts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedCart = await _context.OrderedCarts
                .Include(o => o.OrdersHistory)
                .FirstOrDefaultAsync(m => m.OrderedCartId == id);
            if (orderedCart == null)
            {
                return NotFound();
            }

            return View(orderedCart);
        }

        // GET: OrderedCarts/Create
        public IActionResult Create()
        {
            ViewData["OrdersHistoryId"] = new SelectList(_context.OrdersHistories, "OrderHistoryId", "OrderHistoryId");
            return View();
        }

        // POST: OrderedCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderedCartId,OrdersHistoryId,OrderDate")] OrderedCart orderedCart)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderedCart);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrdersHistoryId"] = new SelectList(_context.OrdersHistories, "OrderHistoryId", "OrderHistoryId", orderedCart.OrdersHistoryId);
            return View(orderedCart);
        }

        // GET: OrderedCarts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedCart = await _context.OrderedCarts.FindAsync(id);
            if (orderedCart == null)
            {
                return NotFound();
            }
            ViewData["OrdersHistoryId"] = new SelectList(_context.OrdersHistories, "OrderHistoryId", "OrderHistoryId", orderedCart.OrdersHistoryId);
            return View(orderedCart);
        }

        // POST: OrderedCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderedCartId,OrdersHistoryId,OrderDate")] OrderedCart orderedCart)
        {
            if (id != orderedCart.OrderedCartId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderedCart);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderedCartExists(orderedCart.OrderedCartId))
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
            ViewData["OrdersHistoryId"] = new SelectList(_context.OrdersHistories, "OrderHistoryId", "OrderHistoryId", orderedCart.OrdersHistoryId);
            return View(orderedCart);
        }

        // GET: OrderedCarts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedCart = await _context.OrderedCarts
                .Include(o => o.OrdersHistory)
                .FirstOrDefaultAsync(m => m.OrderedCartId == id);
            if (orderedCart == null)
            {
                return NotFound();
            }

            return View(orderedCart);
        }

        // POST: OrderedCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderedCart = await _context.OrderedCarts.FindAsync(id);
            if (orderedCart != null)
            {
                _context.OrderedCarts.Remove(orderedCart);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderedCartExists(int id)
        {
            return _context.OrderedCarts.Any(e => e.OrderedCartId == id);
        }
    }
}
