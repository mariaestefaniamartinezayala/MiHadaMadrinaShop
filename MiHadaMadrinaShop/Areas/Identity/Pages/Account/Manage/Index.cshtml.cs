// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly MiHadaMadrinaHandMadeDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public IndexModel(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            MiHadaMadrinaHandMadeDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            /// 


            //-------------- CAMPOS NUEVOS -----------------

            [Display(Name = "Nombre")]
            public string? Nombre { get; set; }

            [Display(Name = "Apellidos")]
            public string? Apellidos { get; set; }

            [Display(Name = "Fecha de nacimiento")]
            public DateTime? FechaNacimiento { get; set; }

            [Display(Name = "Sexo")]
            public byte? IdSexo { get; set; }
            public List<Sexo> listSexos { get; set; }


            //-------------------------------------------

            [Phone]
            [Display(Name = "Teléfono")]
            public string PhoneNumber { get; set; }


        }

        private async Task LoadAsync(IdentityUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            AspNetUser usuario = _context.AspNetUsers.Where(q => q.Id.Equals(user.Id)).FirstOrDefault();
            
            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Nombre = usuario.Nombre,
                Apellidos = usuario.Apellidos,
                IdSexo = usuario.IdSexo,
                FechaNacimiento = usuario.FechaNacimiento
               
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            //List<Sexo> listSexos = _context.Sexos.ToList();

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            await _signInManager.RefreshSignInAsync(user);

            //Añadir guardado AspNetUser

            AspNetUser usuario = _context.AspNetUsers.Where(q => q.Id.Equals(user.Id)).FirstOrDefault();

            if (Input.Nombre != usuario.Nombre)
            {
                usuario.Nombre = Input.Nombre;
            }

            if (Input.Apellidos != usuario.Apellidos)
            {
                usuario.Apellidos = Input.Apellidos;
            }

            if (Input.FechaNacimiento != usuario.FechaNacimiento)
            {
                usuario.FechaNacimiento = Input.FechaNacimiento;
            }

            if (Input.IdSexo != usuario.IdSexo)
            {
                usuario.IdSexo = Input.IdSexo;
            }

            

            //Actuallizamos y guardamos en la base de datos
            _context.AspNetUsers.UpdateRange(usuario);
            await _context.SaveChangesAsync();


            StatusMessage = "Your profile has been updated";
            return RedirectToPage();
        }

        
    }
}


//// Licensed to the .NET Foundation under one or more agreements.
//// The .NET Foundation licenses this file to you under the MIT license.
//#nullable disable

//using System;
//using System.ComponentModel.DataAnnotations;
//using System.Text.Encodings.Web;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
//using Microsoft.AspNetCore.Mvc.RazorPages;
//using Microsoft.AspNetCore.Mvc.Rendering;
//using Microsoft.EntityFrameworkCore;
//using MiHadaMadrinaShop.Models;

//namespace MiHadaMadrinaShop.Areas.Identity.Pages.Account.Manage
//{
//    public class IndexModel : PageModel
//    {
//        private readonly UserManager<IdentityUser> _userManager;
//        private readonly SignInManager<IdentityUser> _signInManager;
//        private readonly AspNetUser _usuario;//Añadimos el contexto para poder añadir los nuevos campos al usuario
//        private readonly MiHadaMadrinaHandMadeDBContext _context;

//        public IndexModel(
//            UserManager<IdentityUser> userManager,
//            SignInManager<IdentityUser> signInManager,
//            AspNetUser usuario,
//            MiHadaMadrinaHandMadeDBContext context)
//        {
//            _userManager = userManager;
//            _signInManager = signInManager;
//            _usuario = usuario;
//            _context = context;
//        }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        public string Username { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        [TempData]
//        public string StatusMessage { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        [BindProperty]
//        public InputModel Input { get; set; }

//        /// <summary>
//        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//        ///     directly from your code. This API may change or be removed in future releases.
//        /// </summary>
//        public class InputModel
//        {
//            /// <summary>
//            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
//            ///     directly from your code. This API may change or be removed in future releases.
//            /// </summary>

//            [Required]
//            [DataType(DataType.Text)]
//            [Display(Name = "Nombre")]
//            public string Nombre { get; set; }

//            [Required]
//            [DataType(DataType.Text)]
//            [Display(Name = "Apellidos")]
//            public string Apellidos { get; set; }

//            [Required]
//            [Display(Name = "Fecha de nacimiento")]
//            [DataType(DataType.Date)]
//            public DateTime? FechaNacimiento { get; set; }

//            [Display(Name = "Teléfono")]
//            [RegularExpression("(\\+34|0034|34)?[ -]*(6|7|8|9)[ -]*([0-9][ -]*){8}")]
//            public string Telefono { get; set; }

//            //[Phone]
//            //[Display(Name = "Phone number")]
//            //public string PhoneNumber { get; set; }
//        }

//        private async Task LoadAsync(IdentityUser user)
//        {
//            var userName = await _userManager.GetUserNameAsync(user);
//            var telefono = await _userManager.GetPhoneNumberAsync(user);

//            Username = userName;

//            Input = new InputModel
//            {
//                Nombre = _usuario.Nombre,
//                Apellidos = _usuario.Apellidos,
//                FechaNacimiento = _usuario.FechaNacimiento,
//                Telefono = telefono
//            };
//        }

//        public async Task<IActionResult> OnGetAsync()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            await LoadAsync(user);
//            return Page();
//        }

//        public async Task<IActionResult> OnPostAsync()
//        {
//            var user = await _userManager.GetUserAsync(User);
//            if (user == null)
//            {
//                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
//            }

//            if (!ModelState.IsValid)
//            {
//                await LoadAsync(user);
//                return Page();
//            }

//            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
//            if (Input.Telefono != phoneNumber)
//            {
//                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.Telefono);
//                if (!setPhoneResult.Succeeded)
//                {
//                    StatusMessage = "Unexpected error when trying to set phone number.";
//                    return RedirectToPage();
//                }
//            }

//            if (Input.Nombre != _usuario.Nombre)
//            {
//                _usuario.Nombre = Input.Nombre;
//            }

//            if (Input.Apellidos != _usuario.Apellidos)
//            {
//                _usuario.Apellidos = Input.Apellidos;
//            }

//            if (Input.FechaNacimiento != _usuario.FechaNacimiento)
//            {
//                _usuario.FechaNacimiento = Input.FechaNacimiento;
//            }

//            //Actuallizamos y guardamos en la base de datos
//            _context.AspNetUsers.UpdateRange(_usuario);
//            await _context.SaveChangesAsync();

//            await _signInManager.RefreshSignInAsync(user);
//            StatusMessage = "Your profile has been updated";
//            return RedirectToPage();
//        }
//    }
//}
