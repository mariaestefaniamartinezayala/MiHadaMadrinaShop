using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiHadaMadrinaShop.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdEstado { get; set; }

        [Display(Name = "Estado")]
        public string Estado1 { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
