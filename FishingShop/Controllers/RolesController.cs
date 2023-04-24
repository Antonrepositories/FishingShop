using FishingShop.Models;
using FishingShop.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FishingShop.Controllers
{
	public class RolesController : Controller
	{
		RoleManager<Role> _roleManager;
		UserManager<ApplicationUser> _userManager;
		public RolesController(RoleManager<Role> roleManager, UserManager<ApplicationUser> userManager)
		{
			_roleManager = roleManager;
			_userManager = userManager;
		}
		[Authorize(Roles ="admin")]
		public IActionResult Index()
		{
			var roles = _roleManager.Roles.ToList();
			return View(roles);
		}
		[Authorize(Roles ="admin")]
		public IActionResult UserList()
		{
			var users = _userManager.Users.ToList();
			return View(users);
		}
		[Authorize(Roles = "admin")]

		public async Task<IActionResult> Edit(int userId)
		{
			ApplicationUser user = _userManager.Users.Where(p => p.Id == userId).ToList()[0];
			if (user != null)
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				var allRoles = _roleManager.Roles.ToList();
				ChangeRoleViewModel model = new ChangeRoleViewModel
				{
					UserId = user.Id,
					UserEnail = user.Email,
					UserRoles = userRoles,
					Allroles = allRoles
				};
				return View(model);
			}
			return NotFound();
		}
		[Authorize(Roles = "admin")]

		[HttpPost, ActionName("Edit")]
		public async Task<IActionResult> Edit(int userId, List<string> Roles)
		{
			ApplicationUser user = _userManager.Users.Where(p => p.Id == userId).ToList()[0];
			if (user != null)
			{
				var userRoles = await _userManager.GetRolesAsync(user);
				var allRoles = _roleManager.Roles.ToList();
				var addedRoles = Roles.Except(userRoles);
				var removedRoles = userRoles.Except(Roles);

				await _userManager.AddToRolesAsync(user, addedRoles);
				await _userManager.RemoveFromRolesAsync(user, removedRoles);
				return RedirectToAction("UserList");
			}
			return NotFound();
		}
	}
}
