using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class FormasDeEnvio
    {
        public FormasDeEnvio()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public byte IdFormaDeEnvio { get; set; }
        public string FormaDeEnvio { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
