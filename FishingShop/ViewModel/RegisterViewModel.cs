using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
namespace FishingShop.ViewModel
{
	public class RegisterViewModel
	{
		[Required, DataType(DataType.EmailAddress), Remote(action:"VerifyEmail", controller:"Account")]
		public string Email { get; set; }
		[Required(ErrorMessage = "Please provide your name")]
		public string Name { get; set; }
		[Required(ErrorMessage ="Please provide your surname")]
		public string Surname { get; set; }
		[Required]
		[DataType(DataType.Password, ErrorMessage ="Incorrect password")]
		public string Password { get; set; }
	}
}
