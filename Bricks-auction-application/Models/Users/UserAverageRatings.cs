using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Users
{
    [Table("UserAverageRatings")]
    public class UserAverageRating
    {
        [Key]
        public int UserId { get; set; }

        public double AverageRating { get; set; } // Średnia ocena użytkownika
    }
}
