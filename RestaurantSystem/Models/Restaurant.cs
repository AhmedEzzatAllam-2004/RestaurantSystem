using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Restaurant
{
    public int RestaurantId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public string? LogoUrl { get; set; }

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
