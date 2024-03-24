using Bricks_auction_application.Models.Items;
using Bricks_auction_application.Models.Offers;
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
        public int OrderedOfferID { get; set; } // Identyfikator zestawu LEGO

        [ForeignKey("OfferId")]
        public virtual Offer Offer { get; set; } // Zestaw LEGO

        //[Required]
        //public int Quantity { get; set; } // Ilość zamówionych zestawów LEGO
    }
}
