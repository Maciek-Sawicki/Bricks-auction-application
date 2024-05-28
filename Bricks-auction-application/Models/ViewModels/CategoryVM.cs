using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Sets;

namespace Bricks_auction_application.Models.ViewModels
{
    public class CategoryVM
    {
        public Category category { get; set; }
        public IFormFile ImageFile { get; set; } // Dodane pole
    }
}
