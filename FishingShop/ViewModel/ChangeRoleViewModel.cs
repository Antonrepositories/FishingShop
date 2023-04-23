using FishingShop.Models;

namespace FishingShop.ViewModel
{
	public class ChangeRoleViewModel
	{
		public int UserId { get; set; }
		public string? UserEnail { get; set; }
		public List<Role> Allroles { get; set; }
		public IList<string> UserRoles { get; set;}
		public ChangeRoleViewModel()
		{
			Allroles = new List<Role>();
			UserRoles = new List<string>();
		}
	}
}
