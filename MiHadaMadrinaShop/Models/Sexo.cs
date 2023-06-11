using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiHadaMadrinaShop.Models
{
    public partial class Sexo
    {
        public Sexo()
        {
            AspNetUsers = new HashSet<AspNetUser>();
        }

        public byte IdSexo { get; set; }

        [Display(Name = "Sexo")]
        public string Sexo1 { get; set; } = null!;

        public virtual ICollection<AspNetUser> AspNetUsers { get; set; }
    }
}
