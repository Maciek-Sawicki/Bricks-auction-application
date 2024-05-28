using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bricks_auction_application.Models.Sets
{
    [Table("Categories")]
    public class Category
    {
        [Key]
        [ValidateNever]
        public int Id { get; set; }

        [Required]
        [ValidateNever]
        public string CategoryName { get; set; }
        [ValidateNever]
        public string ImagePath { get; set; }
    }
}
