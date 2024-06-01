using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Users;

namespace Bricks_auction_application.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OrderedCartItemsController : Controller
    {
        private readonly BricksAuctionDbContext _context;

        public OrderedCartItemsController(BricksAuctionDbContext context)
        {
            _context = context;
        }

        // GET: OrderedCartItems
        public async Task<IActionResult> Index()
        {
            var bricksAuctionDbContext = _context.OrderedCartItems.Include(o => o.Offer).Include(o => o.OrderedCart);
            return View(await bricksAuctionDbContext.ToListAsync());
        }

        // GET: OrderedCartItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedCartItem = await _context.OrderedCartItems
                .Include(o => o.Offer)
                .Include(o => o.OrderedCart)
                .FirstOrDefaultAsync(m => m.OrderedCartItemId == id);
            if (orderedCartItem == null)
            {
                return NotFound();
            }

            return View(orderedCartItem);
        }

        // GET: OrderedCartItems/Create
        public IActionResult Create()
        {
            ViewData["OrderedOfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId");
            ViewData["OrderedCartId"] = new SelectList(_context.OrderedCarts, "OrderedCartId", "OrderedCartId");
            return View();
        }

        // POST: OrderedCartItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderedCartItemId,OrderedCartId,OrderedOfferId")] OrderedCartItem orderedCartItem)
        {
            if (ModelState.IsValid)
            {
                _context.Add(orderedCartItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["OrderedOfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", orderedCartItem.OrderedOfferId);
            ViewData["OrderedCartId"] = new SelectList(_context.OrderedCarts, "OrderedCartId", "OrderedCartId", orderedCartItem.OrderedCartId);
            return View(orderedCartItem);
        }

        // GET: OrderedCartItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedCartItem = await _context.OrderedCartItems.FindAsync(id);
            if (orderedCartItem == null)
            {
                return NotFound();
            }
            ViewData["OrderedOfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", orderedCartItem.OrderedOfferId);
            ViewData["OrderedCartId"] = new SelectList(_context.OrderedCarts, "OrderedCartId", "OrderedCartId", orderedCartItem.OrderedCartId);
            return View(orderedCartItem);
        }

        // POST: OrderedCartItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderedCartItemId,OrderedCartId,OrderedOfferId")] OrderedCartItem orderedCartItem)
        {
            if (id != orderedCartItem.OrderedCartItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(orderedCartItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderedCartItemExists(orderedCartItem.OrderedCartItemId))
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
            ViewData["OrderedOfferId"] = new SelectList(_context.Offers, "OfferId", "OfferId", orderedCartItem.OrderedOfferId);
            ViewData["OrderedCartId"] = new SelectList(_context.OrderedCarts, "OrderedCartId", "OrderedCartId", orderedCartItem.OrderedCartId);
            return View(orderedCartItem);
        }

        // GET: OrderedCartItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderedCartItem = await _context.OrderedCartItems
                .Include(o => o.Offer)
                .Include(o => o.OrderedCart)
                .FirstOrDefaultAsync(m => m.OrderedCartItemId == id);
            if (orderedCartItem == null)
            {
                return NotFound();
            }

            return View(orderedCartItem);
        }

        // POST: OrderedCartItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var orderedCartItem = await _context.OrderedCartItems.FindAsync(id);
            if (orderedCartItem != null)
            {
                _context.OrderedCartItems.Remove(orderedCartItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderedCartItemExists(int id)
        {
            return _context.OrderedCartItems.Any(e => e.OrderedCartItemId == id);
        }
    }
}
