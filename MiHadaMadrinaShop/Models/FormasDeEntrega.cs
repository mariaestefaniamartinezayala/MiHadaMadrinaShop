using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class FormasDeEntrega
    {
        public FormasDeEntrega()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public byte IdFormaDeEntrega { get; set; }
        public string FormaDeEntrega { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
