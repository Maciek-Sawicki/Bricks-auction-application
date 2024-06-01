using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models.Users
{
    [Table("OrdersHistories")]
    public class OrdersHistory
    {
        [Key]
        public int OrderHistoryId { get; set; }

        [ForeignKey("UserId")]
        public string UserId { get; set; }

        // Relacja z użytkownikiem
        
        [ValidateNever]
        public virtual User User { get; set; }

        // Lista zamówionych koszyków w historii zamówień
        [ValidateNever]
        public virtual ICollection<OrderedCart> OrderedCarts { get; set; }

       
    }
}
