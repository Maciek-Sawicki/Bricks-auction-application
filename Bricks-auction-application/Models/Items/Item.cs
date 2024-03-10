using Bricks_auction_application.Models.Auctions;
using Bricks_auction_application.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Items
{
    [Table("Items")]
    public class Item
    {
        [Key]
        public int ItemId { get; set; }

        [Required]
        public string Name { get; set; }

        // Pozostałe właściwości przedmiotu
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsSold { get; set; }

        // Trzeba dodać więcej właściwości

        // Relacje
        public virtual ICollection<Rating> Ratings { get; set; }
        public virtual Auction Auction { get; set; }
    }
}
