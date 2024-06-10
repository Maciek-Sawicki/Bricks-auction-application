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

            // Pobierz przedmioty w koszyku dla bieżącego użytkownika
            var cartItems = await _cartItemRepository.GetAllAsync(
                filter: ci => ci.Cart.UserId == userId,
                includeProperties: "Offer,Offer.LEGOSet,Offer.User"
            );


            var allOffers = cartItems.Select(ci => ci.Offer);

            var viewModel = new OrderSummaryViewModel
            {
                Offers = allOffers.Distinct(), 
            };

            return View(viewModel);
        }

        public IActionResult OrderSummary()
        {
            var cartItems = _cartItemRepository.GetAll(); 

            var groupedCartItems = cartItems.GroupBy(item => item.Offer.User.Email);

            var viewModel = new OrderSummaryViewModel
            {
                GroupedCartItems = groupedCartItems
            };

            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> PlaceOrderAndSendEmail(OrderSummaryViewModel model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            // Sprawdzenie poprawności modelu
            //if (!ModelState.IsValid)
            //{
                // Ponownie pobieramy elementy koszyka w przypadku błędów walidacji
                var cartItems = await _cartItemRepository.GetAllAsync(
                    filter: ci => ci.Cart.UserId == userId,
                    includeProperties: "Offer,Offer.LEGOSet,Offer.User"
                );

                model.Offers = cartItems.Select(ci => ci.Offer).Distinct();
                model.TotalCartValue = model.Offers.Sum(offer => offer.Price);
                model.TotalCartValueWithShipping = model.Offers.Sum(offer => offer.Price + offer.ShippingPrice);

                //return View("Index", model);
            //}

            // Pobierz oferty w koszyku użytkownika
            var cartItemsDb = await _cartItemRepository.GetAllAsync(
                filter: ci => ci.Cart.UserId == userId,
                includeProperties: "Offer,Offer.LEGOSet,Offer.User"
            );

            var allOffers = cartItemsDb.Select(ci => ci.Offer).Distinct();

            var userEmail = User.FindFirstValue(ClaimTypes.Email);
            var subject = "Potwierdzenie zamówienia";
            var message = "<h2>Twoje zamówienie zostało przyjęte</h2>" +
                          "<p>Oto szczegóły zamówienia:</p>" +
                          "<ul>";

            foreach (var offer in allOffers)
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

            // Usuń elementy koszyka po złożeniu zamówienia
            await _cartItemRepository.RemoveAllAsync(ci => ci.Cart.UserId == userId);
            await _cartItemRepository.SaveAsync();

            return View("~/Areas/Customer/Views/OrderConfirmation.cshtml");
        }



    }

}

