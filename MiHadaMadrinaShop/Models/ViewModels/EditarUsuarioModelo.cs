using Microsoft.AspNetCore.Authorization.Infrastructure;
using System.ComponentModel.DataAnnotations;

namespace MiHadaMadrinaShop.Models.ViewModels
{
    public class EditarUsuarioModelo
    {
        public EditarUsuarioModelo()
        {
            Roles = new List<string>();
            Notificaciones = new List<string>();
        }

        public string Id { get; set; }

        [Required]
        public string NombreUsuario { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
        public IList<string> Roles { get; set; }

        // En entity son las claims
        public List<string> Notificaciones { get; set; }
    }
}
