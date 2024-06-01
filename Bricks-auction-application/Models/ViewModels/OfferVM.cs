using Bricks_auction_application.Models.Offers;
using Microsoft.AspNetCore.Http;

namespace Bricks_auction_application.Models.ViewModels
{
    public class OfferVM
    {
        public Offer Offer { get; set; }

        public string UserId { get; set; }
        public IFormFile ImageFile { get; set; } // Dodane pole
    }
}
