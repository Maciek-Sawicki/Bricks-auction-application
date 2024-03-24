using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bricks_auction_application.Models.Users
{
    [Table("OrderedCarts")]
    public class OrderedCart
    {
        [Key]
        public int OrderedCartId { get; set; }

        [ForeignKey("UserId")]
        public int OrdersHistoryId { get; set; }

        [ValidateNever]
        public virtual OrdersHistory OrdersHistory { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [ValidateNever]
        // Lista produktów w zamówionym koszyku
        public virtual ICollection<OrderedCartItem> Items { get; set; }

    }
}



