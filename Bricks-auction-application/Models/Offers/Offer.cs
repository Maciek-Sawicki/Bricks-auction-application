﻿using Bricks_auction_application.Models.Users; // Dodaj to
using Bricks_auction_application.Models.Items;
using Bricks_auction_application.Models.Sets;

using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bricks_auction_application.Models.Offers
{
    [Table("Offers")]
    public class Offer
    {
        [Key]
        public int OfferId { get; set; }

        [Required]
        public int UserId { get; set; } // Identyfikator użytkownika wystawiającego ofertę

        // Relacja z użytkownikiem
        [ForeignKey("UserId")]
        [ValidateNever]
        public User User { get; set; }

        [Required]
        public int LEGOSetId { get; set; } // Numer zestawu LEGO, który jest przedmiotem oferty

        // Relacja z zestawem LEGO
        [ForeignKey("LEGOSetId")]
        [ValidateNever]
        public Set LEGOSet { get; set; }

        //[Required]
        //public int CategoryId { get; set; } // Identyfikator kategorii zestawu LEGO

        // Relacja z kategorią zestawu LEGO
        //[ForeignKey("CategoryId")]
        //public virtual Category Category { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; } // Cena oferty

        //[Required]
        //public int Quantity { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [Required]
        public DateTime OfferEndDateTime { get; set; } // Data i godzina zakończenia oferty
    }
}
