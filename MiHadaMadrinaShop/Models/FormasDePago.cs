using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiHadaMadrinaShop.Models
{
    public partial class FormasDePago
    {
        public FormasDePago()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public byte IdFormaDePago { get; set; }

        [Display(Name = "Forma de pago")]
        public string FormaDePago { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
