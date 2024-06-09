using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Bricks_auction_application.Models;
using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Sets;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using Bricks_auction_application.Models.ViewModels;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity;
using Bricks_auction_application.Models.Users;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Microsoft.AspNetCore.Authorization;

namespace Bricks_auction_application.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class OffersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<User> _userManager;

        public OffersController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        // GET: Offers
        public async Task<IActionResult> Index(string sortOrder, string searchString, decimal? minPrice, decimal? maxPrice, int? minYear, int? maxYear, int? minPieces, int? maxPieces, string sortDirection, int? categoryId)
        {
            // Przypisanie wartości zmiennym ViewData
            ViewData["CurrentSortOrder"] = sortOrder;
            ViewData["CurrentSortDirection"] = sortDirection;
            ViewData["CurrentSearchString"] = searchString;
            ViewData["CurrentMinPrice"] = minPrice;
            ViewData["CurrentMaxPrice"] = maxPrice;
            ViewData["CurrentMinYear"] = minYear;
            ViewData["CurrentMaxYear"] = maxYear;
            ViewData["CurrentMinPieces"] = minPieces;
            ViewData["CurrentMaxPieces"] = maxPieces;
            ViewData["CurrentCategoryId"] = categoryId;

            // Pobranie wszystkich kategorii
            var categories = _unitOfWork.Category.GetAll();
            ViewBag.Categories = new SelectList(categories, "Id", "CategoryName");

            var sets= _unitOfWork.Set.GetAll();
            ViewBag.sets = new SelectList(sets, "Id","Pieces", "ReleaseYear");

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
            offers = FilterOffers(offers, searchString, minPrice, maxPrice, minPieces, maxPieces, minYear, maxYear);

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

        private IQueryable<Offer> FilterOffers(IQueryable<Offer> offers, string searchString, decimal? minPrice, decimal? maxPrice, int? minPieces, int? maxPieces, int? minYear, int? maxYear)
        {

            if (!string.IsNullOrEmpty(searchString))
            {
                searchString = searchString.ToLower();
                offers = offers.Where(o => o.LEGOSet.Name.ToLower().Contains(searchString) ||
                                           o.LEGOSet.SetId.ToString().ToLower().Contains(searchString));
            }

            if (minPrice != null)
            {
                offers = offers.Where(o => o.Price >= minPrice);
            }

            if (maxPrice != null)
            {
                offers = offers.Where(o => o.Price <= maxPrice);
            }

         
            
            if (minPieces != null)
            {
                offers = offers.Where(o => o.LEGOSet.Pieces >= minPieces);
            }

            if (maxPieces != null)
            {
                offers = offers.Where(o => o.LEGOSet.Pieces <= maxPieces);
            }

            if (minYear != null)
            {
                offers = offers.Where(o => o.LEGOSet.ReleaseYear >= minYear);
            }

            if (maxYear != null)
            {
                offers = offers.Where(o => o.LEGOSet.ReleaseYear <= maxYear);
            }
            

            return offers;
        }


        // GET: Offers/Details/5
        // GET: Offers/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var offer = await _unitOfWork.Offer.GetFirstOrDefaultAsync(o => o.OfferId == id, includeProperties: "LEGOSet");
            if (offer == null)
            {
                return NotFound();
            }

            return View(offer);
        }

        // POST: Offers/Details/5
        //[HttpPost]
        //[Authorize]
        //public IActionResult AddToCartDetails(int offerId)
        //{
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    Cart cartFromDb = _unitOfWork.Cart.GetFirstOrDefault(c => c.UserId == userId);
        //    if (cartFromDb != null)
        //    {
        //        // Użytkownik ma już koszyk, więc dodajemy nową pozycję do istniejącego koszyka
        //        CartItem cartItem = new CartItem
        //        {
        //            CartId = cartFromDb.CartId,
        //            OfferId = offerId
        //        };
        //        _unitOfWork.CartItem.Add(cartItem);
        //    }
        //    else
        //    {
        //        // Użytkownik nie ma jeszcze koszyka, więc tworzymy nowy koszyk i dodajemy do niego pozycję
        //        Cart newCart = new Cart
        //        {
        //            UserId = userId,
        //            Items = new List<CartItem>()
        //        };
        //        CartItem cartItem = new CartItem
        //        {
        //            Cart = newCart,
        //            OfferId = offerId
        //        };
        //        _unitOfWork.Cart.Add(newCart);
        //        _unitOfWork.CartItem.Add(cartItem);
        //    }

        //    _unitOfWork.Save();
        //    TempData["success"] = "Cart updated successfully";

        //    return RedirectToAction(nameof(Index));
        //}

        //[HttpPost]
        //[Authorize]
        //public IActionResult AddToCartDetails(int offerId)
        //{
        //    var claimsIdentity = (ClaimsIdentity)User.Identity;
        //    var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

        //    Cart cartFromDb = _unitOfWork.Cart.GetFirstOrDefault(c => c.UserId == userId, includeProperties: "Items");
        //    if (cartFromDb != null)
        //    {
        //        // Sprawdź, czy oferta już istnieje w koszyku
        //        var existingCartItem = cartFromDb.Items.FirstOrDefault(ci => ci.OfferId == offerId);

        //        if (existingCartItem != null)
        //        {
        //            TempData["success"] = "Ta oferta już znajduje się w Twoim koszyku.";
        //            return RedirectToAction(nameof(Index));
        //        }

        //        // Użytkownik ma już koszyk, więc dodajemy nową pozycję do istniejącego koszyka
        //        var cartItem = new CartItem
        //        {
        //            CartId = cartFromDb.CartId,
        //            OfferId = offerId
        //        };

        //        _unitOfWork.CartItem.Add(cartItem);
        //    }
        //    else
        //    {
        //        // Użytkownik nie ma jeszcze koszyka, więc tworzymy nowy koszyk i dodajemy do niego pozycję
        //        var newCart = new Cart
        //        {
        //            UserId = userId,
        //            Items = new List<CartItem>()
        //        };

        //        var cartItem = new CartItem
        //        {
        //            Cart = newCart,
        //            OfferId = offerId
        //        };

        //        _unitOfWork.Cart.Add(newCart);
        //        _unitOfWork.CartItem.Add(cartItem);
        //    }

        //    _unitOfWork.Save();
        //    TempData["success"] = "Cart updated successfully";

        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddToCartDetails(int offerId)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Uzyskanie ID zalogowanego użytkownika

            // Pobieramy koszyk użytkownika
            var cart = await _unitOfWork.Cart.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                // Jeśli koszyk nie istnieje, tworzymy nowy dla użytkownika
                cart = new Cart { UserId = userId };
                _unitOfWork.Cart.Add(cart);
                await _unitOfWork.SaveAsync(); // Zapisujemy nowy koszyk, aby uzyskać CartId
            }

            // Sprawdź, czy oferta już istnieje w koszyku
            var existingCartItem = cart.Items.FirstOrDefault(ci => ci.OfferId == offerId);
            if (existingCartItem != null)
            {
                TempData["success"] = "Ta oferta już znajduje się w Twoim koszyku.";
                return RedirectToAction(nameof(Index));
            }

            // Dodajemy nową pozycję do koszyka
            var cartItem = new CartItem
            {
                CartId = cart.CartId,
                OfferId = offerId
            };
            _unitOfWork.CartItem.Add(cartItem);
            await _unitOfWork.SaveAsync();

            TempData["success"] = "Oferta została pomyślnie dodana do koszyka.";
            return RedirectToAction(nameof(Index));
        }



        // GET: Offers/Create
        public IActionResult Create()
        {
            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "SetId");
            // Pobierz zalogowanego użytkownika
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserId"] = userId;
            return View(new OfferVM { UserId = userId });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OfferVM offerVM, IFormFile ImageFile)
        {
            // tutaj nie waliduje i to trzeba ogarnac
            if (true)
            {
                var currentUser = await _userManager.GetUserAsync(User);
                if (currentUser == null)
                {
                    // Obsłuż sytuację, gdy użytkownik nie jest zalogowany
                    //return RedirectToAction("Login", "Account");
                    return RedirectToAction("Login", "Account", new { area = "Identity" });
                }

                // Przypisz UserId do oferty
                offerVM.Offer.UserId = currentUser.Id.ToString();
                System.Diagnostics.Debug.WriteLine(currentUser.UserId.ToString());
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
            return View(offerVM);
        }

        // GET: Offers/Edit/5
        public async Task<IActionResult> Edit(int? id)
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

            ViewData["LEGOSetId"] = new SelectList(_unitOfWork.Set.GetAll(), "Id", "Name", offer.LEGOSetId);
            return View(new OfferVM
            {
                Offer = offer,
                UserId = offer.UserId
            });
        }


        // GET: Offers/Edit/5
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

            // Ensure UserId is assigned
            var currentUser = await _userManager.GetUserAsync(User);
            if (currentUser == null)
            {
                // Handle the situation where the user is not logged in
                return RedirectToAction("Login", "Account");
            }
            offerVM.Offer.UserId = currentUser.Id;

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
