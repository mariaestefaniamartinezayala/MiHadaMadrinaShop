using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using MiHadaMadrinaShop.Models;
using MiHadaMadrinaShop.Models.ViewModels;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.Users
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UsersController(
            MiHadaMadrinaHandMadeDBContext context, 
            RoleManager<IdentityRole> roleManager, 
            UserManager<IdentityUser> userManager, 
            SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return _context.AspNetUsers != null ?
                        View(await _context.AspNetUsers.ToListAsync()) :
                        Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.AspNetUsers'  is null.");
        }

        [HttpGet]
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(RegistroModelo model)
        {
            if (ModelState.IsValid)
            {
                // Volcamos los datos de la clase RegistroModelo a la clase IdentityUser

                var usuario = new IdentityUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                // Añadimos el usuario en la base de datos AspNetUsers
                var resultado = await _userManager.CreateAsync(usuario, model.Password);

                // Si el usuario se ha creado correctamente y el login es OK, redirigimos a home
                if (resultado.Succeeded)
                {
                    // Redirige a la la p&aacute;gina INDEX de los usuarios
                    return RedirectToAction(nameof(Index));
                }

                // Controlamos el error en caso de que se produzca
                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(string id)
        {
            //Buscamos el usuario por ID
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                ViewBag.ErrorMessage = $"EL usuario con el ID: {id}, no existe en la base de datos";
                return View("Error");
            }

            // Obtenemos la lista de las notificaciones del usuario 
            var usuarioNotificaciones = await _userManager.GetClaimsAsync(usuario);

            // Obtenemos una lista de los roles asignados al usuario
            var usuarioRoles = await _userManager.GetRolesAsync(usuario);

            var model = new EditarUsuarioModelo
            {
                Id = usuario.Id,
                Email = usuario.Email,
                NombreUsuario = usuario.UserName,
                Notificaciones = usuarioNotificaciones.Select(c => c.Value).ToList(),
                Roles = usuarioRoles
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditarUsuarioModelo model)
        {
            //Buscamos el usuario por ID
            var usuario = await _userManager.FindByIdAsync(model.Id);

            if (usuario == null)
            {
                ViewBag.ErrorMessage = $"EL usuario con el ID: {model.Id}, no existe en la base de datos";
                return View("Error");
            }
            else
            {
                usuario.Email = model.Email;
                usuario.UserName = model.NombreUsuario;

                var resultado = await _userManager.UpdateAsync(usuario);

                if (resultado.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);

            if (usuario == null)
            {
                ViewBag.ErrorMessage = $"EL usuario con el ID: {usuario.Id}, no existe en la base de datos";
                return View("Error");
            }
            else
            {
                var resultado = await _userManager.DeleteAsync(usuario);

                if (resultado.Succeeded)
                {
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in resultado.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return RedirectToAction(nameof(Index));
        }

        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(string id)
        //{
        //    if (_context.AspNetUsers == null)
        //    {
        //        return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.AspNetUsers'  is null.");
        //    }
        //    var aspNetUser = await _context.AspNetUsers.FindAsync(id);
        //    if (aspNetUser != null)
        //    {
        //        _context.AspNetUsers.Remove(aspNetUser);
        //    }

        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private bool AspNetUserExists(string id)
        {
            return (_context.AspNetUsers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
/*
    [HttpGet]
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.AspNetUsers == null)
            {
                return NotFound();
            }

            var user = await _context.AspNetUsers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.AspNetUsers == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.AspNetUsers'  is null.");
            }
            var aspNetUser = await _context.AspNetUsers.FindAsync(id);
            if (aspNetUser != null)
            {
                _context.AspNetUsers.Remove(aspNetUser);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
 */