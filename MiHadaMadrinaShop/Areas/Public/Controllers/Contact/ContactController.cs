using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

namespace MiHadaMadrinaShop.Areas.Public.Controllers.Contact
{
    [Area("Public")]
    public class ContactController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult IndexPost()
        {
            return View();
        }
    }
}
