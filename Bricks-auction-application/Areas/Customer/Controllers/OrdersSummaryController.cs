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
        private readonly IEmailSender _emailSender;

        public OrdersSummaryController(IOrderHeaderRepository orderHeaderRepository, ICartItemRepository cartItemRepository, IEmailSender emailSender)
        {
            _orderHeaderRepository = orderHeaderRepository;
            _cartItemRepository = cartItemRepository;
            _emailSender = emailSender;
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

        [HttpPost]
        public async Task<IActionResult> PlaceOrderAndSendEmail()
        {
            // Tutaj dodaj logikę zapisywania zamówienia do bazy danych
            // np. używając _orderHeaderRepository

            // Następnie skomponuj treść wiadomości e-mail
            //var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var cartItems = await _cartItemRepository.GetAllAsync(
            filter: ci => ci.Cart.UserId == userId,
            includeProperties: "Offer,Offer.LEGOSet,Offer.User"
            );

            var allOffers = cartItems.Select(ci => ci.Offer);

            var viewModel = new OrderSummaryViewModel
            {
                Offers = allOffers.Distinct(), // Usuń duplikaty ofert
                // Pozostałe informacje na temat zamówienia
            };


            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var subject = "Potwierdzenie zamówienia";
            var message = "<h2>Twoje zamówienie zostało przyjęte</h2>" +
              "<p>Oto szczegóły zamówienia:</p>" +
              "<ul>";

            foreach (var offer in viewModel.Offers)
            {
                message += "<li>" +
                           "<strong>Nazwa:</strong> " + offer.LEGOSet.Name + "<br/>" +
                           "<strong>Cena:</strong> " + offer.Price.ToString("C") + "<br/>" +
                           "<strong>Cena z wysyłką:</strong> " + (offer.Price + offer.ShippingPrice).ToString("C") + "<br/>" +
                           "<strong>Sprzedający:</strong> " + offer.User.Email + "<br/>" +
                           "<strong>Numer do przelewu:</strong> " + offer.User.AccountNumber + "<br/><br/>" +
                           "</li>";
            }

            message += "</ul>";


            // Wyślij e-mail
            await _emailSender.SendEmailAsync(userEmail, subject, message);

            User.FindFirstValue(ClaimTypes.NameIdentifier);
            await _cartItemRepository.RemoveAllAsync(ci => ci.Cart.UserId == userId);
            await _cartItemRepository.SaveAsync();

            // Przekieruj użytkownika na inną stronę po złożeniu zamówienia
            return View("~/Areas/Customer/Views/OrderConfirmation.cshtml");
        }



    }

}

