using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Factura
    {
        public long IdFactura { get; set; }
        public long IdPedido { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; } = null!;
    }
}
