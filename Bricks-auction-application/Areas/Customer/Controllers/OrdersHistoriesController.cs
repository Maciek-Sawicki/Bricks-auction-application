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
    public class OrdersHistoriesController : Controller
    {
        private readonly BricksAuctionDbContext _context;

        public OrdersHistoriesController(BricksAuctionDbContext context)
        {
            _context = context;
        }

        // GET: OrdersHistories
        public async Task<IActionResult> Index()
        {
            var bricksAuctionDbContext = _context.OrdersHistories.Include(o => o.User);
            return View(await bricksAuctionDbContext.ToListAsync());
        }

        // GET: OrdersHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordersHistory = await _context.OrdersHistories
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderHistoryId == id);
            if (ordersHistory == null)
            {
                return NotFound();
            }

            return View(ordersHistory);
        }

        // GET: OrdersHistories/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: OrdersHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderHistoryId,UserId")] OrdersHistory ordersHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ordersHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", ordersHistory.UserId);
            return View(ordersHistory);
        }

        // GET: OrdersHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordersHistory = await _context.OrdersHistories.FindAsync(id);
            if (ordersHistory == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", ordersHistory.UserId);
            return View(ordersHistory);
        }

        // POST: OrdersHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderHistoryId,UserId")] OrdersHistory ordersHistory)
        {
            if (id != ordersHistory.OrderHistoryId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ordersHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrdersHistoryExists(ordersHistory.OrderHistoryId))
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
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", ordersHistory.UserId);
            return View(ordersHistory);
        }

        // GET: OrdersHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ordersHistory = await _context.OrdersHistories
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderHistoryId == id);
            if (ordersHistory == null)
            {
                return NotFound();
            }

            return View(ordersHistory);
        }

        // POST: OrdersHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ordersHistory = await _context.OrdersHistories.FindAsync(id);
            if (ordersHistory != null)
            {
                _context.OrdersHistories.Remove(ordersHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrdersHistoryExists(int id)
        {
            return _context.OrdersHistories.Any(e => e.OrderHistoryId == id);
        }
    }
}
