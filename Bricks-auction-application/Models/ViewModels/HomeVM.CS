using System.Collections.Generic;
using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Sets;

namespace Bricks_auction_application.Models.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Offer> Offers { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}
