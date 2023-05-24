using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }
        public long IdProductoPedido { get; set; }
        public string? Comentario1 { get; set; }
        public bool Publicar { get; set; }
        public byte? Valoracion { get; set; }

        public virtual ProductosPedido IdProductoPedidoNavigation { get; set; } = null!;
    }
}
