using Microsoft.AspNetCore.Mvc;

namespace MiHadaMadrinaShop.Areas.Public.Controllers
	{
	public class PrivacyController : Controller
		{
		[Area("Public")]
		public IActionResult Index()
			{
			return View();
			}
		}
	}
