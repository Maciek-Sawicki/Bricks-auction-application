using Bricks_auction_application.Models.Offers;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models.Users
{
    public class OrderHeader
    {
        public string OrderHeaderId { get; set; }
        public string UserId { get; set; }
        [ForeignKey("UserId")]
        [ValidateNever]
        public User user { get; set; }
        public DateTime OrderDate { get; set; }
        public double OrderTotal { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        [Required]
        public string StreetAddress { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string PostalCode { get; set; }
        [Required]
        public string Country { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}