using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bricks_auction_application.Models.Items;

namespace Bricks_auction_application.Models.Users
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [Required]
        public int Quantity { get; set; }

        // Klucz obcy do koszyka
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public virtual Cart Cart { get; set; }

        // Klucz obcy do zestawu LEGO
        [ForeignKey("Set")]
        public int SetId { get; set; }
        public virtual Set Set { get; set; }
    }
}
