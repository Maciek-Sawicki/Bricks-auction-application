using Bricks_auction_application.Models.Items;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Bricks_auction_application.Models.Offers
{
    [Table("Offers")]
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }

        [Required]
        public DateTime EndTime { get; set; }

        [Required]
        public int UserId { get; set; } // Identyfikator użytkownika wystawiającego ofertę

        [Required]
        public int LEGOSetId { get; set; } // Numer zestawu LEGO, który jest przedmiotem oferty

        [Required]
        public int CategoryId { get; set; } // Identyfikator kategorii zestawu LEGO

        [Required]
        public decimal Price { get; set; } // Cena oferty

        [Required]
        public int Quantity { get; set; }

        public DateTime OfferEndDateTime { get; set; } // Data i godzina zakończenia oferty

        // Kategoria zestawu LEGO
        public Category Category { get; set; }
    }
}
