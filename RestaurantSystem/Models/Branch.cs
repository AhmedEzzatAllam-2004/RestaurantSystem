using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Branch
{
    public int BranchId { get; set; }

    public string? Location { get; set; }

    public string? PhoneNumber { get; set; }

    public int? RestaurantId { get; set; }

    public int? CuisineId { get; set; }

    public virtual Cuisine? Cuisine { get; set; }

    public virtual ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Restaurant? Restaurant { get; set; }
}
