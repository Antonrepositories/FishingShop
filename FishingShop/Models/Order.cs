using System;
using System.Collections.Generic;

namespace FishingShop.Models;

public partial class Order
{
    public int IdOrder { get; set; }

    public string Email { get; set; } = null!;

    public int IdUser { get; set; }

    public int Deliverypoint { get; set; }

    public DateTime Date { get; set; }

    public int Status { get; set; }

    public virtual DeliveryPoint DeliverypointNavigation { get; set; } = null!;

    public virtual ApplicationUser IdUserNavigation { get; set; } = null!;
}
