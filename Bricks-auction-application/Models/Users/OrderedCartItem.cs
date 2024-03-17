using Bricks_auction_application.Models.Items;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models.Users
{
    [Table("OrderedCartItems")]
    public class OrderedCartItem
    {
        [Key]
        public int OrderedCartItemId { get; set; }

        [Required]
        public int OrderedCartId { get; set; }

        [ForeignKey("OrderedCartId")]
        public virtual OrderedCart OrderedCart { get; set; }

        [Required]
        public int SetId { get; set; } // Identyfikator zestawu LEGO

        [ForeignKey("SetId")]
        public virtual Set Set { get; set; } // Zestaw LEGO

        [Required]
        public int Quantity { get; set; } // Ilość zamówionych zestawów LEGO
    }
}
