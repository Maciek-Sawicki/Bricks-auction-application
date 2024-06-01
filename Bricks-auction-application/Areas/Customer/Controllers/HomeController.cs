using Microsoft.AspNetCore.Mvc;
using Bricks_auction_application.Models.ViewModels;
using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Rendering;
using Bricks_auction_application.Models.Sets;

namespace Bricks_auction_application.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index(string searchString)
        {
            var offers = _unitOfWork.Offer.GetAll(includeProperties: "LEGOSet,User");
            if (!string.IsNullOrEmpty(searchString))
            {
                offers = offers.Where(o => o.LEGOSet.Name.ToLower().Contains(searchString.ToLower()) || o.LEGOSet.Id.ToString().Contains(searchString.ToLower()));
            }

            var categories = _unitOfWork.Category.GetAllCategories();

            if (categories == null)
            {
                categories = new List<Category>(); // Przypisanie pustej listy kategorii, je�li jest nullem
            }

            var homeViewModel = new HomeViewModel
            {
                Offers = offers,
                Categories = categories
            };

            return View(homeViewModel);
        }




        [HttpPost]
        public IActionResult IndexPost(string searchFilter)
        {
            return RedirectToAction("Index", new { searchString = searchFilter });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult FilterByCategory(int categoryId)
        {
            // Przekieruj do akcji Index w kontrolerze Offers, przekazuj�c ID kategorii jako parametr
            return RedirectToAction("Index", "Offers", new { categoryId });
        }
    }
}
