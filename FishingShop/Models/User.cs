using System;
using System.Collections.Generic;

namespace FishingShop.Models;

public partial class User
{
    public int IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int Role { get; set; }

    public string? Paydata { get; set; }

    public string Number { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; } = new List<Order>();
}
