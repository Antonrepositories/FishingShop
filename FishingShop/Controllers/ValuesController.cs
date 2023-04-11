using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace FishingShop.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ValuesController : ControllerBase
	{
		private readonly ShopDatabaseContext _context;
		public ValuesController(ShopDatabaseContext context)
		{
			_context = context;
		}
		[HttpGet("JsonData")]
		public JsonResult JsonData()
		{
			var categories = _context.Categories.ToList();
			List<Object> catProduct = new List<Object>();
			//var products = _context.Products;
			catProduct.Add(new[] {"Категорія", "Кількість товарів"});
			foreach(var c in categories)
			{
				var product = _context.Products.ToList();
				int products_amount = 0;
				for(int i =0; i < product.Count; i++)
				{
					if (product[i].Category == c.IdCategory)
					{
						products_amount += 1;
					}
				}
				catProduct.Add(new object[] {c.Name, c.Products.Count});
			}
			return new JsonResult(catProduct);
		}
	}
}
