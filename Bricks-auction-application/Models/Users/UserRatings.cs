using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Users
{
    [Table("UserRatings")]
    public class UserRating
    {
        [Key]
        public int UserRatingId { get; set; }

        [Required]
        public int RatedUserId { get; set; } // Identyfikator ocenianego użytkownika
        [ForeignKey("RatedUserId")]
        public virtual User RatedUser { get; set; } // Użytkownik, którego oceniamy

        [Required]
        public int RatingUserId { get; set; } // Identyfikator użytkownika wystawiającego ocenę
        [ForeignKey("RatingUserId")]
        public virtual User RatingUser { get; set; } // Użytkownik, który wystawia ocenę

        [Required]
        public int Rating { get; set; } // Ocena użytkownika

        // Dodatkowe informacje, np. komentarz
        public string Comment { get; set; }
    }
}
