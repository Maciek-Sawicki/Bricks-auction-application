using Bricks_auction_application.Models.Items;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Auctions
{
    [Table("Auctions")]
    public class Auction
    {
        [Key]
        public int AuctionId { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        // Pozostałe właściwości aukcji
        public string Description { get; set; }
        public bool IsActive { get; set; }

        // Trzeba dodać więcej właściwości

        // Relacje
        public virtual ICollection<Item> Items { get; set; }
    }
}
