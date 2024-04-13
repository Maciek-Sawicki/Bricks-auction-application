using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Offers;

namespace Bricks_auction_application.Controllers
{
    public class OffersController : Controller
    {
        private readonly BricksAuctionDbContext _context;

        public OffersController(BricksAuctionDbContext context)
        {
            _context = context;
        }

        // GET: Offers
        /* public async Task<IActionResult> Index()
         {
             var bricksAuctionDbContext = _context.Offers.Include(o => o.LEGOSet).Include(o => o.User);
             return View(await bricksAuctionDbContext.ToListAsync());
         }*/
        public async Task<IActionResult> Index(string sortOrder, string sortDirection)
        {
            ViewData["NameSort"] = sortOrder == "Name" ? "name_desc" : "";
            ViewData["PriceSort"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["OfferEndDateTimeSort"] = sortOrder == "OfferEndDateTime" ? "offerEndDateTime_desc" : "OfferEndDateTime";

            ViewData["CurrentSortOrder"] = sortOrder;
            ViewData["CurrentSortDirection"] = sortDirection;

            var offers = from o in _context.Offers.Include(o => o.User).Include(o => o.LEGOSet)
                         select o;

            switch(sortOrder)
            {
                case "Name":
                    offers = sortDirection == "ascending" ? offers.OrderBy(o => o.LEGOSet.Name) : offers.OrderByDescending(o => o.LEGOSet.Name);
                    break;
                case "Price":
                    offers = sortDirection == "ascending" ? offers.OrderBy(o => o.Price) : offers.OrderByDescending(o => o.Price);
                    break;
                case "OfferEndDateTime":
                    offers = sortDirection == "ascending" ? offers.OrderBy(o => o.OfferEndDateTime) : offers.OrderByDescending(o => o.OfferEndDateTime);
                    break;
                default:
                    offers = offers.OrderBy(o => o.LEGOSet.Name);
                    break;
            }

            return View(await offers.ToListAsync());
        }


        /*public async Task<IActionResult> Index(string sortOrder)
        {
            ViewBag.PriceSortParm = String.IsNullOrEmpty(sortOrder) ? "price_desc" : "";
            ViewBag.EndDateSortParm = sortOrder == "OfferEndDateTime" ? "endDate_desc" : "OfferEndDateTime";

            var offers = from o in _context.Offers
                         select o;

            switch (sortOrder)
            {
                case "price_desc":
                    offers = offers.OrderByDescending(o => o.Price);
                    break;
                case "endDate_desc":
                    offers = offers.OrderByDescending(o => o.OfferEndDateTime);
                    break;
                default:
                    offers = offers.OrderBy(o => o.Price);
                    break;
            }

            return View(await offers.ToListAsync());
        }*/
        // GET: Offers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.LEGOSet)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            ViewData["LEGOSetId"] = new SelectList(_context.Sets, "Id", "Name");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email");
            return View();
        }

        // POST: Offers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* public async Task<IActionResult> Create([Bind("OfferId,UserId,LEGOSetId,Price,OfferEndDateTime")] Offer offer)
         {
             if (ModelState.IsValid)
             {
                 _context.Add(offer);
                 await _context.SaveChangesAsync();
                 return RedirectToAction(nameof(Index));
             }
             ViewData["LEGOSetId"] = new SelectList(_context.Sets, "Id", "Name", offer.LEGOSetId);
             ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", offer.UserId);
             return View(offer);
         }*/
        public async Task<IActionResult> Create([Bind("OfferId,UserId,LEGOSetId,Price,OfferEndDateTime")] Offer offer)
        {
            if (ModelState.IsValid)
            {
                // Pobierz zestaw LEGO dla oferty
                var legoSet = await _context.Sets.FindAsync(offer.LEGOSetId);
                if (legoSet == null)
                {
                    return NotFound("Nie można znaleźć zestawu LEGO dla podanego identyfikatora.");
                }

                // Przypisz kategorię zestawu LEGO jako kategorię oferty
                offer.CategoryId = legoSet.CategoryId;

                // Dodaj ofertę do kontekstu bazy danych i zapisz zmiany
                _context.Add(offer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LEGOSetId"] = new SelectList(_context.Sets, "Id", "Name", offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", offer.UserId);
            return View(offer);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers.FindAsync(id);
            if (offer == null)
            {
                return NotFound();
            }
            ViewData["LEGOSetId"] = new SelectList(_context.Sets, "Id", "Name", offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", offer.UserId);
            return View(offer);
        }

        // POST: Offers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        /* public async Task<IActionResult> Edit(int id, [Bind("OfferId,UserId,LEGOSetId,Price,OfferEndDateTime")] Offer offer)
         {
             if (id != offer.OfferId)
             {
                 return NotFound();
             }

             if (ModelState.IsValid)
             {
                 try
                 {
                     _context.Update(offer);
                     await _context.SaveChangesAsync();
                 }
                 catch (DbUpdateConcurrencyException)
                 {
                     if (!OfferExists(offer.OfferId))
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
             ViewData["LEGOSetId"] = new SelectList(_context.Sets, "Id", "Name", offer.LEGOSetId);
             ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", offer.UserId);
             return View(offer);
         }*/
        public async Task<IActionResult> Edit(int id, [Bind("OfferId,UserId,LEGOSetId,Price,OfferEndDateTime")] Offer offer)
        {
            if (id != offer.OfferId)
            {
                return NotFound();
            }

            // Pobierz zestaw LEGO dla oferty
            var legoSet = await _context.Sets.FindAsync(offer.LEGOSetId);
            if (legoSet == null)
            {
                return NotFound("LEGOSet not found");
            }

            // Przypisz kategorię zestawu LEGO do oferty
            offer.CategoryId = legoSet.CategoryId;

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(offer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OfferExists(offer.OfferId))
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
            ViewData["LEGOSetId"] = new SelectList(_context.Sets, "Id", "Name", offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "Email", offer.UserId);
            return View(offer);
        }
        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = await _context.Offers
                .Include(o => o.LEGOSet)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var offer = await _context.Offers.FindAsync(id);
            if (offer != null)
            {
                _context.Offers.Remove(offer);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OfferExists(int id)
        {
            return _context.Offers.Any(e => e.OfferId == id);
        }
    }
}
