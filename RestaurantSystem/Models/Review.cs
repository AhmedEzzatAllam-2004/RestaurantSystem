using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Review
{
    public int ReviewId { get; set; }

    public int? Rating { get; set; }

    public string? Comment { get; set; }

    public int? OrderId { get; set; }

    public virtual Order? Order { get; set; }
}
