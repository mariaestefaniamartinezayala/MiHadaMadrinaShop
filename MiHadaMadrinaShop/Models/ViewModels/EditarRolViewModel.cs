using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiHadaMadrinaShop.Models.ViewModels
{
    public class EditarRolViewModel
    {
        public string Id { get; set; }

        public List<string> Usuarios { get; set; }

        [Required(ErrorMessage = "El campo Rol es obligatorio.")]
        [Display(Name = "Rol")]
        public string RolNombre { get; set; }

        public EditarRolViewModel()
        {
            Usuarios = new List<string>();
        }
    }

}
