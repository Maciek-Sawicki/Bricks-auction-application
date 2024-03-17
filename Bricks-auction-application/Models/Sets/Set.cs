using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Items
{
    [Table("Sets")]
    public class Set
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SetId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public int ReleaseYear { get; set; }

        [ForeignKey("CategoryId")]
        public int CategoryId { get; set; }
    }
}
