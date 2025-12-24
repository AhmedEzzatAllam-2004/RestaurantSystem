using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public DateTime? OrderDate { get; set; }

    public decimal? TotalAmount { get; set; }

    public int? CustomerId { get; set; }

    public int? BranchId { get; set; }

    public int? PromoId { get; set; }

    public virtual Branch? Branch { get; set; }

    public virtual Customer? Customer { get; set; }

    public virtual Delivery? Delivery { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual Payment? Payment { get; set; }

    public virtual Promotion? Promo { get; set; }

    public virtual Review? Review { get; set; }
}
