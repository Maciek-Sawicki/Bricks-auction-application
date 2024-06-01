using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Identity;

namespace Bricks_auction_application.Models.Users
{
    [Table("Users")]
    public class User : IdentityUser
    {

        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? Email { get; set; }

        public string? AccountNumber { get; set; }
        // Relacja z koszykiem użytkownika
        [ValidateNever]
        public virtual Cart? Cart { get; set; }

        // Relacja z zamówieniami użytkownika
        [ValidateNever]
        public virtual ICollection<OrdersHistory>? OrdersHistory { get; set; }

        // Kolekcja opinii o użytkowniku (sprzedającym)
        /* [ValidateNever]
         public ICollection<SellerReview> SellerReviews { get; set; }*/
    }
}