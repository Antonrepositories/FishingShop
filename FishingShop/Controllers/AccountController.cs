using FishingShop.Models;
using FishingShop.ViewModel;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
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

			if (ModelState.IsValid)
			{
				ApplicationUser user = new ApplicationUser { Email = model.Email, Name = model.Name, Surname = model.Surname, UserName = model.Email };
				var result = await _useUserManager.CreateAsync(user, model.Password);
				if (result.Succeeded)
				{
					await _useUserManager.AddToRoleAsync(user, "user");
					await _signInManager.SignInAsync(user, false);
					Console.WriteLine($"User registered succesfully, his email - {model.Email}");
					return RedirectToAction("Index", "Products");
				}
				else
				{
					return View("Occured");
					//Console.WriteLine($"User registered unsuccesfully, his email - {model.Email}");

					//foreach (var error in result.Errors)
					//{
					//	ModelState.AddModelError(string.Empty, error.Description);
					//	Console.WriteLine($"Error Description -  {error.Description}");
					//}
				}
			}
			else
			{
				return View(model);
			}
			//return RedirectToAction("Index", "Products");
		}
		[HttpGet, ActionName("Login")]
		public IActionResult Login(string returnUrl = null)
		{
			return View(new LoginViewModel { ReturnUrl = returnUrl});
		}
		[HttpPost, ActionName("Login")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			Console.WriteLine("Working method");
			if (ModelState.IsValid)
			{
				var user = _useUserManager.Users.Where(user => user.Email == model.Email).ToList()[0];
				var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, false);
				Console.WriteLine(user.UserName);
				if (result.Succeeded)
				{
					if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
					{
						return Redirect(model.ReturnUrl);
					}
					else
					{
						return RedirectToAction("Index", "Products");
					}
				}
				else
				{
					return View("Occured");
				}
			}
			else
			{
				return View(model);
			}
		}
		[HttpPost, ActionName("Logout")]
		public async Task<IActionResult> Logout(string returnUrl = null)
		{
			await _signInManager.SignOutAsync();
			if (returnUrl != null)
			{
				return LocalRedirect(returnUrl);
			}
			else
			{
				return RedirectToActionPermanent("Login", "Account");
				//return RedirectToPage("/Account", new { area = "Login" });
			}
		}
		[AllowAnonymous]
		[AcceptVerbs("GET", "POST")]
		[ActionName("VerifyEmail")]
		public IActionResult VerifyEmail(string email)
		{
			//var result = await _useUserManager.FindByEmailAsync(email);
			Console.WriteLine("CHINAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
			var user = _useUserManager.Users.Where(user => user.Email == email).ToList();
			if (user.Count == 0)
			{
				return Json(true);
			}
			else
			{
				return Json($"Email {email} is already in use.");
			}
			//if (result == null)
			//{
			//	return Json(true);
			//}
			//else
			//{
			//	return Json($"Email {email} is already in use.");
			//}

			//return Json(true);
		}
	}
}
