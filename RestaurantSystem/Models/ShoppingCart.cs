using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantSystem.Models
{
    public partial class ShoppingCart
    {
        [Key]
        public int CartId { get; set; } 

        public int? CustomerId { get; set; } 

        public int? MenuItemId { get; set; } 

        public int Quantity { get; set; }

        [ForeignKey("MenuItemId")]
        public virtual MenuItem? MenuItem { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer? Customer { get; set; }
    }
}