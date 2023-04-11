using System;
using System.Collections.Generic;

namespace FishingShop.Models;

public partial class OrderItem
{
    public int IdOrderitem { get; set; }

    public int Amount { get; set; }

    public double Price { get; set; }

    public int IdProduct { get; set; }

    public int IdOrder { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;
}
