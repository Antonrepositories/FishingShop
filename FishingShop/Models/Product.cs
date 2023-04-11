#nullable disable
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FishingShop.Models;

public partial class Product
{
    public int IdProduct { get; set; }
    [Required(ErrorMessage ="Product picture is requred")]
    public string Image { get; set; } = null!;
    [Required(ErrorMessage = "Product price is requred")]

    public double Price { get; set; }
    [Required(ErrorMessage = "Product decription is requred")]

    public string Description { get; set; } = null!;
    [ForeignKey("id_category")]
    public int Category { get; set; }
    [Required(ErrorMessage = "Product name is requred")]

    public string Name { get; set; } = null!;

    public virtual Category CategoryNavigation { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; } = new List<OrderItem>();

    public virtual ICollection<Review> Reviews { get; } = new List<Review>();
}
