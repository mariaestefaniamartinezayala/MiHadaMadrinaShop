using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Menu
    {
        public int IdMenu { get; set; }
        public string Controlador { get; set; } = null!;
        public bool EsActivo { get; set; }
        public DateTime FechaRegistro { get; set; }
        public string Icono { get; set; } = null!;
        public string NombreMenu { get; set; } = null!;
        public string PaginaAccion { get; set; } = null!;
    }
}
