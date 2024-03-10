using Bricks_auction_application.Models.Items;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Users
{
    [Table("Rating-users")]
    public class Rating
    {
        [Key]
        public int RatingId { get; set; }

        [Required]
        public int RatingValue { get; set; }

        // Relacje
        [ForeignKey("User")]
        public int GivenByUserId { get; set; }
        public virtual User GivenByUser { get; set; }

        [ForeignKey("Item")]
        public int ItemId { get; set; }
        public virtual Set Item { get; set; }

        public string Comment { get; set; }
        public DateTime DateRated { get; set; }

        // Trzeba dodać więcej właściwości
    }
}
