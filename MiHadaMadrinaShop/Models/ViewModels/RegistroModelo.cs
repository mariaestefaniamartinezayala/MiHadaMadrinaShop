using System.ComponentModel.DataAnnotations;

namespace MiHadaMadrinaShop.Models.ViewModels
{
    public class RegistroModelo
    {
        [Required]
        [EmailAddress]
        [Display(Name = "Correo")]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Contraseña")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirmar contraseña")]
        [Compare("Password", ErrorMessage = "La contraseña no coincide.")]
        public string ConfirmPassword { get; set; }
    }
}
