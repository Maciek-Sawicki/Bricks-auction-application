using Bricks_auction_application.Models.Items;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Bricks_auction_application.Models.Users
{
    [Table("Carts")]
    public class Cart
    {
        [Key]
        [ForeignKey("User")]
        public int UserId { get; set; }

        // Relacja z użytkownikiem
        public virtual User User { get; set; }

        // Lista pozycji w koszyku
        public virtual ICollection<CartItem> Items { get; set; }
    }
}
