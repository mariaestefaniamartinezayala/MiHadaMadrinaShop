using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.AppRoles
{
    [Area("Admin")]
   // [Authorize(Roles = "Admin")]
    public class AppRolesController : Controller
    {
        private readonly RoleManager<IdentityRole> _roleManager;
        public AppRolesController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        //Lista con todos los roles del usuario.
        public IActionResult Index()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole modeloRol)
        {
            //Si no existe el roll, lo creamos
            if (!_roleManager.RoleExistsAsync(modeloRol.Name).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(modeloRol.Name)).GetAwaiter().GetResult();
            }


            return RedirectToAction(nameof(Index));
        }
    }
}
