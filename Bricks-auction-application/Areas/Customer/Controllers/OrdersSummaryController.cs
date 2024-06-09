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
            var orders = await _orderHeaderRepository.GetAllAsync(
                filter: o => o.UserId == userId,
                includeProperties: "OrderDetails.Offer" // Użyj OrderDetails.Offer zamiast CartItem
            );

            // Pobierz przedmioty w koszyku dla bieżącego użytkownika
            var cartItems = await _cartItemRepository.GetAllAsync(
                filter: ci => ci.Cart.UserId == userId,
                includeProperties: "Offer"
            );

            // Zbierz wszystkie oferty z zamówień oraz przedmiotów w koszyku
            var allOffers = orders.SelectMany(o => o.OrderDetails.Select(od => od.Offer))
                                  .Concat(cartItems.Select(ci => ci.Offer));

            var viewModel = new OrderSummaryViewModel
            {
                Offers = allOffers.Distinct(), // Usuń duplikaty ofert
                // Pozostałe informacje na temat zamówienia
            };

            return View(viewModel);
        }
    }
}
