using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Estado
    {
        public Estado()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public int IdEstado { get; set; }
        public string Estado1 { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
