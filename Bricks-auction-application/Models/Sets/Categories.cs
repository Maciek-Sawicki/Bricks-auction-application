using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Sets
{
    [Table("Category")]
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string CategoryName { get; set; }
    }
}
