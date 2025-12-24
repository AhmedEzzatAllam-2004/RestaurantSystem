using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Payment
{
    public int PaymentId { get; set; }

    public decimal? Amount { get; set; }

    public string? Method { get; set; }

    public string? Status { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
