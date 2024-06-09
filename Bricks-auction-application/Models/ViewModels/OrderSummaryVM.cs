using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Users;

namespace Bricks_auction_application.ViewModels
{
    public class OrderSummaryViewModel
    {
        public IEnumerable<CartItem> CartItems { get; set; }
        public IEnumerable<Offer> Offers { get; set; }
        public decimal TotalCartValue { get; set; }
        public decimal TotalCartValueWithShipping { get; set; }

        // Shipping details
        public string Name { get; set; }
        public string PhoneNumber { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public IEnumerable<IGrouping<string?, CartItem>> GroupedCartItems { get; internal set; }
    }
}
