using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Users
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }

        // Trzeba dodać więcej właściwości

        // Relacje
        public virtual ICollection<Rating> RatingsGiven { get; set; }
        public virtual ICollection<Rating> RatingsReceived { get; set; }
        public virtual Cart Cart { get; set; }
    }
}
