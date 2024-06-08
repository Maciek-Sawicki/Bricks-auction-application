using Bricks_auction_application.Models.Users; // Dodaj to
using Bricks_auction_application.Models.Items;
using Bricks_auction_application.Models.Sets;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel;

namespace Bricks_auction_application.Models.Offers
{
    [Table("Offers")]
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }

        [ValidateNever]

        public string UserId { get; set; } // Identyfikator użytkownika wystawiającego ofertę

        // Relacja z użytkownikiem
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }

        [ValidateNever]
        
        public int LEGOSetId { get; set; } // Numer zestawu LEGO, który jest przedmiotem oferty

        // Relacja z zestawem LEGO
        [ForeignKey("LEGOSetId")]
        [Display(Name = "Nazwa")]
        public Set LEGOSet { get; set; }

        //[Required]
        //public int CategoryId { get; set; } // Identyfikator kategorii zestawu LEGO

        // Relacja z kategorią zestawu LEGO
        //[ForeignKey("CategoryId")]
        //public virtual Category Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena")]
        public decimal Price { get; set; } // Cena oferty

        [Column(TypeName = "decimal(18,2)")]
        [Display(Name = "Cena dostawy")]
        public decimal ShippingPrice { get; set; } // Cena oferty

        //[Required]
        //public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        [Display(Name = "Data Zakończenia")]
        public DateTime OfferEndDateTime { get; set; } // Data i godzina zakończenia oferty
        [ValidateNever]
        public string ImagePath { get; set; }
    }
}
