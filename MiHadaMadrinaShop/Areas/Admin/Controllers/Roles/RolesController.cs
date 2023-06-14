using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;
using MiHadaMadrinaShop.Models.ViewModels;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.Roles
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;


        public RolesController(MiHadaMadrinaHandMadeDBContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IdentityRole modeloRol)
        {
            if (!string.IsNullOrEmpty(modeloRol.Name))
            {
                var roleExists = await _roleManager.RoleExistsAsync(modeloRol.Name);
                if (!roleExists)
                {
                    await _roleManager.CreateAsync(new IdentityRole(modeloRol.Name));
                }
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AspNetRoles == null)
            {
                return NotFound();
            }

            var aspNetRole = await _context.AspNetRoles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aspNetRole == null)
            {
                return NotFound();
            }

            return View(aspNetRole);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //Buscamos el rol por ID
            var rol = await _roleManager.FindByIdAsync(id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"El rol con el ID: {id}, no existe en la base de datos";
                return View("Error");
            }

            var model = new EditarRolViewModel
            {
                Id = rol.Id,
                RolNombre = rol.Name,
                Usuarios = new List<string>() 
            };

            //Obtenemos todos los usuarios
            foreach(var usuario in _userManager.Users.ToList())
            {
                if (await _userManager.IsInRoleAsync(usuario, rol.Name))
                {
                    model.Usuarios.Add(usuario.UserName);
                }
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarRolViewModel model)
        {
            //Buscamos el rol por ID
            var rol = await _roleManager.FindByIdAsync(model.Id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"El rol con el ID: {model.Id}, no existe en la base de datos";
                return View("Error");
            }
            else
            {
                rol.Name = model.RolNombre;

                var resultado = await _roleManager.UpdateAsync(rol);

                if(resultado.Succeeded)
                {
                    return RedirectToAction("Index");
                }

                foreach(var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditarUsuarioRol(string rolId)
        {
            ViewBag.roleId = rolId;

            //Buscamos el rol por ID
            var rol = await _roleManager.FindByIdAsync(rolId);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"El rol con el ID: {rolId}, no existe en la base de datos";
                return View("Error");
            }

            var model = new List<UsuarioRolModelo>();

            foreach (var user in _userManager.Users.ToList())
            {
                var usuarioRolModelo = new UsuarioRolModelo
                {
                    UsuarioId = user.Id,
                    NombreUsuario = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, rol.Name))
                {
                    usuarioRolModelo.EstaSeleccionado = true;
                }
                else
                {
                    usuarioRolModelo.EstaSeleccionado = false;
                }

                model.Add(usuarioRolModelo);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditarUsuarioRol(List<UsuarioRolModelo> model, string rolId)
        {
            //Buscamos el rol por ID
            var rol = await _roleManager.FindByIdAsync(rolId);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"El rol con el ID: {rolId}, no existe en la base de datos";
                return View("Error");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UsuarioId);

                if (user == null)
                {
                    ViewBag.ErrorMessage = $"El usuario con el ID: {model[i].UsuarioId} no existe en la base de datos";
                    return RedirectToAction("Error");
                }

                IdentityResult result = null;

                if (model[i].EstaSeleccionado && !(await _userManager.IsInRoleAsync(user, rol.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, rol.Name); 
                }
                else if (!model[i].EstaSeleccionado && await _userManager.IsInRoleAsync(user, rol.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, rol.Name);    
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("Edit", new {Id = rolId});
                    }
                }
            }

            return RedirectToAction("Edit", new { Id = rolId });
        }


        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {

            var rol = await _roleManager.FindByIdAsync(id);

            if (rol == null)
            {
                ViewBag.ErrorMessage = $"EL rol con el ID: {rol.Id}, no existe en la base de datos";
                return View("Error");
            }
            else
            {
                var resultado = await _roleManager.DeleteAsync(rol);

                if (resultado.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in resultado.Errors.ToList())
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        //[HttpGet]
        //public async Task<IActionResult> Delete(string id)
        //{
        //    if (id == null || _context.AspNetRoles == null)
        //    {
        //        return NotFound();
        //    }

        //    var aspNetRole = await _context.AspNetRoles
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (aspNetRole == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(aspNetRole);
        //}

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_context.AspNetRoles == null)
        //    {
        //        return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.AspNetRoles'  is null.");
        //    }
        //    var aspNetRole = await _context.AspNetRoles.FindAsync(id);
        //    if (aspNetRole != null)
        //    {
        //        _context.AspNetRoles.Remove(aspNetRole);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AspNetRoleExists(string id)
        {
            return (_context.AspNetRoles?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
