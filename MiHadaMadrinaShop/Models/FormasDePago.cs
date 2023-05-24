using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class FormasDePago
    {
        public FormasDePago()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public byte IdFormaDePago { get; set; }
        public string FormaDePago { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
