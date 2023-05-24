using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Modulo
    {
        public Modulo()
        {
            Operaciones = new HashSet<Operacione>();
        }

        public byte IdModulo { get; set; }
        public string Modulo1 { get; set; } = null!;

        public virtual ICollection<Operacione> Operaciones { get; set; }
    }
}
