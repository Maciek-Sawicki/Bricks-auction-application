using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Sets
{
    [Table("Categories")]
    public class Categories
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
