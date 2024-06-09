using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using Bricks_auction_application.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models.Offers
{
    public class OrderDetails
    {
        public int Id { get; set; }

        [Required]
        public string OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int OfferId { get; set; }
        [ForeignKey("OfferId")]
        [ValidateNever]
        public Offer Offer { get; set; } // Dodaj właściwość Offer
    }
}
