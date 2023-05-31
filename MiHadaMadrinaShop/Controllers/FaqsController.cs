using Microsoft.AspNetCore.Mvc;

namespace MiHadaMadrinaShop.Controllers
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
