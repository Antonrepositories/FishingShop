using FishingShop.Models;
using Microsoft.AspNetCore.Identity;

namespace FishingShop
{
	public class RoleInitializer
	{
		public static async Task InitializyAsync(UserManager<ApplicationUser> userManager, RoleManager<Role> roleManager)
		{
			string moderEmail = "moder@gmail.com";
			string pass = "P@ss123456";
			string name = "Moder";
			string surname = "Moder";
			var roles = roleManager.Roles.Where(p => p.Name == "admin").ToList();
			if (roles.Count == 0)
			{
				Role role = new Role();
				role.Name = "admin";
				await roleManager.CreateAsync(role);
			}
			roles = roleManager.Roles.Where(p => p.Name == "user").ToList();
			if (roles.Count == 0)
			{
				Role role = new Role();
				role.Name = "user";
				await roleManager.CreateAsync(role);
			}
			if (await userManager.FindByEmailAsync(moderEmail) == null)
			{
				ApplicationUser moder = new ApplicationUser { Email = moderEmail, Name = name, UserName = moderEmail, Surname = surname };
				IdentityResult result = await userManager.CreateAsync(moder, pass);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(moder, "admin");
				}
			}
		}
	}
}
