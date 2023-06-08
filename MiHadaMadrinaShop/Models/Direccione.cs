using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Direccione
    {
        public long IdDireccion { get; set; }
        public string? CodPostal { get; set; }
        public string? Direccion { get; set; }
        public string? Localidad { get; set; }
        public string? Pais { get; set; }
        public string? Provincia { get; set; }
        public string IdAspNetUsers { get; set; } = null!;
        public bool EsFacturacion { get; set; }
        public bool EsDomicilio { get; set; }

        public virtual AspNetUser IdAspNetUsersNavigation { get; set; } = null!;
    }
}
