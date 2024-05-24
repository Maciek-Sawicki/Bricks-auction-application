using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using System.IO;

namespace Bricks_auction_application.Controllers
{
    public class OffersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public OffersController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Offers
        public async Task<IActionResult> Index(string sortOrder, string sortDirection)
        {
            ViewData["NameSort"] = sortOrder == "Name" ? "name_desc" : "";
            ViewData["PriceSort"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["OfferEndDateTimeSort"] = sortOrder == "OfferEndDateTime" ? "offerEndDateTime_desc" : "OfferEndDateTime";
            ViewData["CurrentSortOrder"] = sortOrder;
            ViewData["CurrentSortDirection"] = sortDirection;

            var offers = await _unitOfWork.Offer.GetAllAsync();

            switch (sortOrder)
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

            return View(offers.ToList());
        }

        // GET: Offers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = _unitOfWork.Offer.GetFirstOrDefault(o => o.OfferId == id, includeProperties: "LEGOSet,User");
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer); // Zmieniono z View(offers) na View(offer)
        }
        // GET: Offers/Create
        public IActionResult Create()
        {
            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name");
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email");
            return View();
        }

        // POST: Offers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OfferId,UserId,LEGOSetId,Price,OfferEndDateTime")] Offer offer, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string offerPath = Path.Combine(wwwRootPath, "images", "offer");
                    using (var fileStream = new FileStream(Path.Combine(offerPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    offer.ImagePath = Path.Combine("images", "offer", fileName);
                }

                // Pobierz zestaw LEGO dla oferty
                var legoSet = _unitOfWork.Set.GetFirstOrDefault(s => s.Id == offer.LEGOSetId);
                if (legoSet == null)
                {
                    return NotFound("Nie można znaleźć zestawu LEGO dla podanego identyfikatora.");
                }

                // Przypisz kategorię zestawu LEGO jako kategorię oferty
                offer.CategoryId = legoSet.CategoryId;

                // Dodaj ofertę do kontekstu bazy danych i zapisz zmiany
                _unitOfWork.Offer.Add(offer);
                _unitOfWork.Save();
                TempData["success"] = "Offer created successfully";
                return RedirectToAction(nameof(Index));
            }
            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name", offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", offer.UserId);
            return View(offer);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = _unitOfWork.Offer.GetFirstOrDefault(o => o.OfferId == id);
            if (offer == null)
            {
                return NotFound();
            }
            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name", offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", offer.UserId);
            return View(offer);
        }

        // POST: Offers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OfferId,UserId,LEGOSetId,Price,OfferEndDateTime")] Offer offer)
        {
            if (id != offer.OfferId)
            {
                return NotFound();
            }

            // Pobierz zestaw LEGO dla oferty
            var legoSet = _unitOfWork.Set.GetFirstOrDefault(s => s.Id == offer.LEGOSetId);
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
                    _unitOfWork.Offer.Update(offer);
                    _unitOfWork.Save();
                    TempData["success"] = "Offer updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.Offer.Exists(offer.OfferId))
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
            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name", offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", offer.UserId);
            return View(offer);
        }

        // GET: Offers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var offer = _unitOfWork.Offer.GetFirstOrDefault(o => o.OfferId == id, includeProperties: "LEGOSet,User");
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
            var offer = _unitOfWork.Offer.GetFirstOrDefault(o => o.OfferId == id);
            if (offer != null)
            {
                _unitOfWork.Offer.Remove(offer);
                _unitOfWork.Save();
                TempData["success"] = "Offer deleted successfully";
            }
            return RedirectToAction(nameof(Index));
        }
    }
}