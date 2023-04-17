using FishingShop.Models;
using FishingShop.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FishingShop.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<ApplicationUser> _useUserManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		public AccountController(UserManager<ApplicationUser> useUserManager, SignInManager<ApplicationUser> signInManager)
		{
			_useUserManager = useUserManager;
			_signInManager = signInManager;
		}
		[HttpGet, ActionName("Register")]
		public IActionResult Register()
		{
			return View();
		}
		[HttpPost, ActionName("Register")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			Console.WriteLine($"User registered succesfully, his email - Just method");

			if (ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser { Email = model.Email, Name = model.Name, Surname = model.Surname };
				var result = await _useUserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await _signInManager.SignInAsync(user, false);
					Console.WriteLine($"User registered succesfully, his email - {model.Email}");
					return RedirectToAction("Index", "Products");
				}
				else
				{
					Console.WriteLine($"User registered unsuccesfully, his email - {model.Email}");

					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error.Description);
						Console.WriteLine($"Error Description -  {error.Description}");
					}
				}
			}
			return RedirectToAction("Index", "Products");
		}
		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			return RedirectToAction("Index", "Products");
		}
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Products");
				}
			}
			return View(model);
		}
	}
}
