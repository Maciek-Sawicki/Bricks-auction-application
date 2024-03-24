using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Bricks_auction_application.Models.Offers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bricks_auction_application.Models.Users
{
    [Table("CartItems")]
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        //[Required]
        //public int Quantity { get; set; }

        // Klucz obcy do koszyka
        [ForeignKey("Cart")]
        public int CartId { get; set; }
        [ValidateNever]
        public virtual Cart Cart { get; set; }

        // Klucz obcy do oferty
        [ForeignKey("Offer")]
        public int OfferId { get; set; }
        [ValidateNever]
        public virtual Offer Offer { get; set; }
    }
}
