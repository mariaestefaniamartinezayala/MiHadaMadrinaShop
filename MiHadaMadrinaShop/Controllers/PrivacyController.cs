using Microsoft.AspNetCore.Mvc;

namespace MiHadaMadrinaShop.Controllers
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
