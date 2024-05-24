using Bricks_auction_application.Models.Offers;
using Bricks_auction_application.Models.Users;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Bricks_auction_application.Models.Sets;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

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

        
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
    }
}
