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
using Bricks_auction_application.Models.ViewModels;

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
        public async Task<IActionResult> Index(string sortOrder, string searchString, decimal? minPrice, decimal? maxPrice, string sortDirection, int? categoryId)
        {
            // Przypisanie wartości zmiennym ViewData
            ViewData["CurrentSortOrder"] = sortOrder;
            ViewData["CurrentSortDirection"] = sortDirection;
            ViewData["CurrentSearchString"] = searchString;
            ViewData["CurrentMinPrice"] = minPrice;
            ViewData["CurrentMaxPrice"] = maxPrice;
            ViewData["CurrentCategoryId"] = categoryId;

            // Pobranie wszystkich kategorii
            var categories = _unitOfWork.Category.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

            // Pobranie ofert z repozytorium
            var offers = (await _unitOfWork.Offer.GetAllAsync()).AsQueryable();

            // Filtruj według kategorii
            if (categoryId.HasValue && categoryId > 0)
            {
                offers = offers.Where(o => o.CategoryId == categoryId);
            }

            // Zastosowanie sortowania
            offers = SortOffers(offers, sortOrder, sortDirection);

            // Filtruj oferty
            offers = FilterOffers(offers, searchString, minPrice, maxPrice);

            // Przekazanie ofert do widoku
            return View(offers.ToList());
        }


        private IQueryable<Offer> SortOffers(IQueryable<Offer> offers, string sortOrder, string sortDirection)
        {
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
            return offers;
        }

        private IQueryable<Offer> FilterOffers(IQueryable<Offer> offers, string searchString, decimal? minPrice, decimal? maxPrice)
        {
            if (!String.IsNullOrEmpty(searchString))
            {
                offers = offers.Where(s => s.LEGOSet.Name.Contains(searchString));
            }

            if (minPrice != null)
            {
                offers = offers.Where(o => o.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                offers = offers.Where(o => o.Price <= maxPrice);
            }

            return offers;
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

            return View(offer);
        }

        // GET: Offers/Create
        public IActionResult Create()
        {
            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name");
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email");
            return View(new OfferVM());
        }
        // POST: Offers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create(OfferVM offerVM, IFormFile ImageFile)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                // Upload Image
                if (ImageFile != null && ImageFile.Length > 0)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                    string folderPath = Path.Combine(wwwRootPath, "images", "offer");
                    string filePath = Path.Combine(folderPath, fileName);

                    if (!Directory.Exists(folderPath))
                    {
                        Directory.CreateDirectory(folderPath);
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await ImageFile.CopyToAsync(fileStream);
                    }

                    offerVM.Offer.ImagePath = Path.Combine("images", "offer", fileName).Replace("\\", "/");
                }

                // Get LEGO set
                var legoSet = _unitOfWork.Set.GetFirstOrDefault(s => s.Id == offerVM.Offer.LEGOSetId);
                if (legoSet == null)
                {
                    return NotFound("Nie można znaleźć zestawu LEGO dla podanego identyfikatora.");
                }

                // Assign LEGO set category to offer
                offerVM.Offer.CategoryId = legoSet.CategoryId;

                // Add offer to database
                _unitOfWork.Offer.Add(offerVM.Offer);
                _unitOfWork.Save();
                TempData["success"] = "Offer created successfully";
                return RedirectToAction(nameof(Index));
            }

            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name", offerVM.Offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", offerVM.Offer.UserId);
            return View(offerVM);
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

            var offerVM = new OfferVM
            {
                Offer = offer
            };

            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name", offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", offer.UserId);

            return View(offerVM);
        }

        // POST: Offers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, OfferVM offerVM, IFormFile ImageFile)
        {
            if (id != offerVM.Offer.OfferId)
            {
                return NotFound();
            }

            // Get LEGO set
            var legoSet = _unitOfWork.Set.GetFirstOrDefault(s => s.Id == offerVM.Offer.LEGOSetId);
            if (legoSet == null)
            {
                return NotFound("LEGOSet not found");
            }

            // Assign LEGO set category to offer
            offerVM.Offer.CategoryId = legoSet.CategoryId;

            if (ModelState.IsValid)
            {
                try
                {
                    string wwwRootPath = _webHostEnvironment.WebRootPath;

                    // Upload Image
                    if (ImageFile != null && ImageFile.Length > 0)
                    {
                        string fileName = Guid.NewGuid().ToString() + Path.GetExtension(ImageFile.FileName);
                        string folderPath = Path.Combine(wwwRootPath, "images", "offer");
                        string filePath = Path.Combine(folderPath, fileName);

                        if (!Directory.Exists(folderPath))
                        {
                            Directory.CreateDirectory(folderPath);
                        }

                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await ImageFile.CopyToAsync(fileStream);
                        }

                        offerVM.Offer.ImagePath = Path.Combine("images", "offer", fileName).Replace("\\", "/");
                    }

                    // Update offer in database
                    _unitOfWork.Offer.Update(offerVM.Offer);
                    _unitOfWork.Save();
                    TempData["success"] = "Offer updated successfully";
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_unitOfWork.Offer.Exists(offerVM.Offer.OfferId))
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

            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name", offerVM.Offer.LEGOSetId);
            ViewData["UserId"] = new SelectList(_unitOfWork.User.GetAll(), "UserId", "Email", offerVM.Offer.UserId);
            return View(offerVM);
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
