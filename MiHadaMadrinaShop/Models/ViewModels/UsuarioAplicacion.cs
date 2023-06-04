using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace MiHadaMadrinaShop.Models.ViewModels
{
    public class UsuarioAplicacion : IdentityUser
    {
        // [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Nombre")]
        public string? Nombre { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Apellidos")]
        public string? Apellidos { get; set; }

        [Display(Name = "Fecha de nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? FechaNacimiento { get; set; }

        [Display(Name = "Teléfono")]
        [RegularExpression("(\\+34|0034|34)?[ -]*(6|7|8|9)[ -]*([0-9][ -]*){8}")]
        public string? Telefono { get; set; }

        public string? Sexo { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? SexosList { get; set; }

        public string? Rol { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem>? RolesList { get; set; }

        public string? ImagenUrl { get; set; }

    }
}
