using System.ComponentModel.DataAnnotations;
using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Users;
using System.Collections.Generic;
using System.Linq;

namespace Bricks_auction_application.ViewModels
{
    public class OrderSummaryViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public decimal TotalCartValue { get; set; }
        public decimal TotalCartValueWithShipping { get; set; }

        // Shipping details with validation
        [Required(ErrorMessage = "Imię i nazwisko są wymagane.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Numer telefonu jest wymagany.")]
        [RegularExpression(@"^\+?[0-9\s\-]{6,15}$", ErrorMessage = "Wprowadź poprawny numer telefonu.")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Adres jest wymagany.")]
        public string StreetAddress { get; set; }

        [Required(ErrorMessage = "Miasto jest wymagane.")]
        public string City { get; set; }

        [Required(ErrorMessage = "Kod pocztowy jest wymagany.")]
        [RegularExpression(@"^\d{2}-\d{3}$", ErrorMessage = "Wprowadź poprawny kod pocztowy w formacie 00-000.")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Kraj jest wymagany.")]
        public string Country { get; set; }

        public IEnumerable<IGrouping<string?, CartItem>> GroupedCartItems { get; internal set; }
    }
}
