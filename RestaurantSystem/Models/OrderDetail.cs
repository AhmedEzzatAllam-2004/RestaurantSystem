using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class OrderDetail
{
    public int OrderId { get; set; }

    public int ItemId { get; set; }

    public int? Quantity { get; set; }

    public decimal? Price { get; set; }

    public virtual MenuItem Item { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;
}
