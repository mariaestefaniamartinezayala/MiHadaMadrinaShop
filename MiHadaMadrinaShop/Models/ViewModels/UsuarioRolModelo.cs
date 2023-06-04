using Microsoft.AspNetCore.Builder;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiHadaMadrinaShop.Models.ViewModels
{
    public class UsuarioRolModelo
    {
        public string UsuarioId { get; set; }

        public string NombreUsuario { get; set; }

        public bool EstaSeleccionado { get; set; }

    }

}
