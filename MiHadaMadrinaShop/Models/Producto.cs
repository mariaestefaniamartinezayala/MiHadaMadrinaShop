using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Producto
    {
        public Producto()
        {
            ProductosPedidos = new HashSet<ProductosPedido>();
            IdCategoria = new HashSet<Categoria>();
        }

        public long IdProducto { get; set; }
        public string? DescripcionCorta { get; set; }
        public string? DescripcionLarga { get; set; }
        public DateTime FechaDeEntrada { get; set; }
        public string? Imagen { get; set; }
        public string Nombre { get; set; } = null!;
        public int? PorcentajeDeDescuento { get; set; }
        public decimal Precio { get; set; }
        public decimal? PrecioConDescuento { get; set; }
        public int? Stock { get; set; }
        public string? UrlProductoDigital { get; set; }

        public virtual ICollection<ProductosPedido> ProductosPedidos { get; set; }

        public virtual ICollection<Categoria> IdCategoria { get; set; }
    }
}
