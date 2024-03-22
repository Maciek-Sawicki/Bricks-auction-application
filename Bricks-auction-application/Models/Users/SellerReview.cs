using Bricks_auction_application.Models.Users;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models
{
    [Table("SellerReviews")]
    public class SellerReview
    {
        [Key]
        public int ReviewId { get; set; }

        [Required]
        public int SellerUserId { get; set; } // Id sprzedającego, który jest oceniany

        [Required]
        public int Rating { get; set; } // Ocena

        [Required]
        public string ReviewText { get; set; } // Treść opinii

        [Required]
        public DateTime ReviewDate { get; set; } // Data opinii

        // Relacja z użytkownikiem (sprzedającym), który jest oceniany
        [ForeignKey("SellerUserId")]
        public User Seller { get; set; }
    }
}
