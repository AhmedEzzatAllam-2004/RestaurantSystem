using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class DeliveryAgent
{
    public int AgentId { get; set; }

    public string Name { get; set; } = null!;

    public string? Phone { get; set; }

    public string? VehicleType { get; set; }

    public virtual ICollection<Delivery> Deliveries { get; set; } = new List<Delivery>();
}
