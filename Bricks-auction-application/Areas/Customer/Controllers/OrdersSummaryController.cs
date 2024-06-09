using Bricks_auction_application.Models.System.Repository.IRepository;
using Bricks_auction_application.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Bricks_auction_application.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class OrdersSummaryController : Controller
    {
        private readonly IOrderHeaderRepository _orderHeaderRepository;
        private readonly ICartItemRepository _cartItemRepository;

        public OrdersSummaryController(IOrderHeaderRepository orderHeaderRepository, ICartItemRepository cartItemRepository)
        {
            _orderHeaderRepository = orderHeaderRepository;
            _cartItemRepository = cartItemRepository;
        }

        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Pobierz zamówienia dla bieżącego użytkownika
            //var orders = await _orderHeaderRepository.GetAllAsync(
            //    filter: o => o.UserId == userId,
            //    includeProperties: "OrderDetails.Offer, OrderDetails.Offer.UserId" // Użyj OrderDetails.Offer zamiast CartItem
            //);

            // Pobierz przedmioty w koszyku dla bieżącego użytkownika
            var cartItems = await _cartItemRepository.GetAllAsync(
                filter: ci => ci.Cart.UserId == userId,
                includeProperties: "Offer,Offer.LEGOSet,Offer.User"
            );

            // Zbierz wszystkie oferty z zamówień oraz przedmiotów w koszyku
            //var allOffers = orders.SelectMany(o => o.OrderDetails.Select(od => od.Offer))
            //                      .Concat(cartItems.Select(ci => ci.Offer));

            var allOffers = cartItems.Select(ci => ci.Offer);

            var viewModel = new OrderSummaryViewModel
            {
                Offers = allOffers.Distinct(), // Usuń duplikaty ofert
                // Pozostałe informacje na temat zamówienia
            };

            return View(viewModel);
        }

        public IActionResult OrderSummary()
        {
            // Retrieve the cart items
            var cartItems = _cartItemRepository.GetAll(); // Assuming you have a method to retrieve cart items

            // Group cart items by seller's email
            var groupedCartItems = cartItems.GroupBy(item => item.Offer.User.Email);

            // Create a view model to pass to the view
            var viewModel = new OrderSummaryViewModel
            {
                GroupedCartItems = groupedCartItems
            };

            return View(viewModel);
        }

    }
}
