using System;
using System.Collections.Generic;

namespace FishingShop.Models;

public partial class DeliveryPoint
{
    public int IdPoint { get; set; }

    public string Image { get; set; } = null!;

    public string Adress { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
