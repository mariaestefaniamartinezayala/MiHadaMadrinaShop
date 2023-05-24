using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class ProductosPedido
    {
        public ProductosPedido()
        {
            Comentarios = new HashSet<Comentario>();
        }

        public long IdProductoPedido { get; set; }
        public long IdPedido { get; set; }
        public long IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal Precio { get; set; }

        public virtual Pedido IdPedidoNavigation { get; set; } = null!;
        public virtual Producto IdProductoNavigation { get; set; } = null!;
        public virtual ICollection<Comentario> Comentarios { get; set; }
    }
}
