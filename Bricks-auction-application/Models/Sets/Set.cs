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
        [Display(Name = "Numer katalogowy")]
        public string SetId { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Display(Name = "Nazwa angielska")]
        public string EnglishName { get; set; }

        [Display(Name = "Opis")]
        public string Description { get; set; }

        [Display(Name = "Liczba elementów")]
        public int Pieces { get; set; }

        [Display(Name = "Liczba minifigurek")]
        public int Minifigures { get; set; }
        [Required]
        [Display(Name = "Rok wydania")]
        public int ReleaseYear { get; set; }

        
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        [Display(Name = "Kategoria")]
        public Category Category { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena katalogowa")]
        public decimal ListPrice { get; set; }
        public string ImagePath { get; set; }
    }
}
