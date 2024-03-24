using Bricks_auction_application.Models.Items;
using Bricks_auction_application.Models.Offers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models.Users
{
    [Table("OrderedCartItems")]
    public class OrderedCartItem
    {
        [Key]
        public int OrderedCartItemId { get; set; }

        [ForeignKey("OrderedCartId")]
        public int OrderedCartId { get; set; }
        [ValidateNever]
        public virtual OrderedCart OrderedCart { get; set; }

        [Required]
        public int OrderedOfferId { get; set; } // Identyfikator oferty

        [ForeignKey("OrderedOfferId")]
        [ValidateNever]
        public virtual Offer Offer { get; set; } // Zestaw LEGO

        //[Required]
        //public int Quantity { get; set; } // Ilość zamówionych zestawów LEGO
    }
}
