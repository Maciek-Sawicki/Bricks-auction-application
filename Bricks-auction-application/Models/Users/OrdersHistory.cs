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
        public int OrderId { get; set; }

        [Required]
        public int UserId { get; set; }

        // Lista zamówionych koszyków w historii zamówień
        public virtual ICollection<OrderedCart> OrderedCarts { get; set; }

        // Relacja z użytkownikiem
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }
}
