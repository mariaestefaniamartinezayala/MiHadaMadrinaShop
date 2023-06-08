using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Sexo
    {
        public Sexo()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        public byte IdSexo { get; set; }
        public string Sexo1 { get; set; } = null!;

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
