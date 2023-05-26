using Microsoft.AspNetCore.Mvc;

namespace MiHadaMadrinaShop.Areas.Public.Controllers
{
	[Area("Public")]
	public class FaqsController : Controller
		{
		public IActionResult Index()
			{
			return View();
			}
		}
	}
