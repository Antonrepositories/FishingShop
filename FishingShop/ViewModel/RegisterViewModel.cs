using System.ComponentModel.DataAnnotations;
namespace FishingShop.ViewModel
{
	public class RegisterViewModel
	{
		[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string Surname { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
