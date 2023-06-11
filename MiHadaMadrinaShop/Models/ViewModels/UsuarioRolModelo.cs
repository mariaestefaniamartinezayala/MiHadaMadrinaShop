using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiHadaMadrinaShop.Models.ViewModels
{
    public class UsuarioRolModelo
    {
        public string UsuarioId { get; set; }

        [Display(Name = "Nombre de usuario")]
        public string NombreUsuario { get; set; }

        public bool EstaSeleccionado { get; set; }

    }

}
