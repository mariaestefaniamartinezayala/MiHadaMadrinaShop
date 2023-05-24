using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Operacione
    {
        public byte IdOperacion { get; set; }
        public byte IdModulo { get; set; }
        public string Operacion { get; set; } = null!;

        public virtual Modulo IdModuloNavigation { get; set; } = null!;
    }
}
