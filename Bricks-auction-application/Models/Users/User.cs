
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bricks_auction_application.Models.Users
{
    [Table("Users")]
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        // Relacja z koszykiem użytkownika
        [ValidateNever]
        public virtual Cart Cart { get; set; }

        // Relacja z zamówieniami użytkownika
        //[ValidateNever]
        public virtual ICollection<OrdersHistory> OrdersHistory { get; set; }

        // Kolekcja opinii o użytkowniku (sprzedającym)
        //[ValidateNever]
        public ICollection<SellerReview> SellerReviews { get; set; }
    }
}
