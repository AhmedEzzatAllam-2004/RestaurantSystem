using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Address
{
    public int AddressId { get; set; }

    public string? Street { get; set; }

    public string? City { get; set; }

    public string? Notes { get; set; }

    public int? CustomerId { get; set; }

    public virtual Customer? Customer { get; set; }
}
