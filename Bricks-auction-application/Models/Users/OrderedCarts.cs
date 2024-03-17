using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models.Users
{
    [Table("OrderedCarts")]
    public class OrderedCart
    {
        [Key]
        public int OrderedCartId { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        // Lista produktów w zamówionym koszyku
        public virtual ICollection<OrderedCartItem> Items { get; set; }

        // Relacja z użytkownikiem
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
