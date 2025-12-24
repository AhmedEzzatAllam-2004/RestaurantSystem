using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Cuisine
{
    public int CuisineId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Branch> Branches { get; set; } = new List<Branch>();
}
