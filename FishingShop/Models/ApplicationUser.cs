using Microsoft.AspNetCore.Identity;

namespace FishingShop.Models
{
	public class ApplicationUser : IdentityUser<int>
	{
		public string Name { get; set; }
		public string Surname { get; set; }
		public virtual ICollection<Order> Orders { get; } = new List<Order>();
	}
}
