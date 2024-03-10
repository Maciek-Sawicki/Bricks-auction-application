using Bricks_auction_application.Models.Items;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Users
{
    [Table("Carts")]
    public class Cart
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Relacja
        public virtual User User { get; set; }

        // Trzeba dodać więcej właściwości
        public virtual ICollection<Set> Items { get; set; }
    }
}
