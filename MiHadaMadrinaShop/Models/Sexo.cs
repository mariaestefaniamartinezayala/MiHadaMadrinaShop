using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Sexo
    {
        public Sexo()
        {
            DatosUsuarios = new HashSet<DatosUsuario>();
        }

        public byte IdSexo { get; set; }
        public string Sexo1 { get; set; } = null!;

        public virtual ICollection<DatosUsuario> DatosUsuarios { get; set; }
    }
}
