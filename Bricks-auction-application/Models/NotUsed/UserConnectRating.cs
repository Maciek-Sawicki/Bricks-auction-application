using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models.Users
{
    [Table("UserUserRatings")]
    public class UserConnectRating
    {
        [Key]
        public int UserUserRatingId { get; set; }

        [Required]
        public int UserId { get; set; } // Identyfikator użytkownika

        [Required]
        public int UserRatingId { get; set; } // Identyfikator oceny użytkownika

        // Właściwości nawigacyjne
        public virtual User User { get; set; }
        public virtual UserRating UserRating { get; set; }
    }
}