using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Promotion
{
    public int PromoId { get; set; }

    public string Code { get; set; } = null!;

    public decimal? DiscountPercentage { get; set; }

    public DateOnly? ExpiryDate { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
