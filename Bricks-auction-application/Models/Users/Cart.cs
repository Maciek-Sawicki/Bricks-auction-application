using Bricks_auction_application.Models.Items;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Bricks_auction_application.Models.Users
{
    [Table("Carts")]
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

        // Relacja z użytkownikiem
        [ValidateNever]
        public virtual User User { get; set; }
        [ValidateNever]
        // Lista pozycji w koszyku
        public virtual ICollection<CartItem> Items { get; set; }
    }
}
