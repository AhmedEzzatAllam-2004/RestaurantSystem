using System;
using System.Collections.Generic;

namespace RestaurantSystem.Models;

public partial class Delivery
{
    public int DeliveryId { get; set; }

    public string? Status { get; set; }

    public int? OrderId { get; set; }

    public int? AgentId { get; set; }

    public virtual DeliveryAgent? Agent { get; set; }

    public virtual Order? Order { get; set; }
}
