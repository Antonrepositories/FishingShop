#nullable disable
using System;
using System.Collections.Generic;

namespace FishingShop.Models;

public partial class Review
{
    public int IdReview { get; set; }

    public string Text { get; set; } = null!;

    public int Rating { get; set; }

    public int IdProduct { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;
}
