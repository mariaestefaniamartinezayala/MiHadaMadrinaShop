using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            Facturas = new HashSet<Factura>();
            ProductosPedidos = new HashSet<ProductosPedido>();
        }

        public long IdPedido { get; set; }
        public long IdDireccion { get; set; }
        public int IdEstado { get; set; }
        public byte IdFormaDeEntrega { get; set; }
        public byte IdFormaDeEnvio { get; set; }
        public byte IdFormaDePago { get; set; }
        public decimal Iva { get; set; }
        public byte? PorcentajeDescuento { get; set; }
        public decimal Total { get; set; }
        public decimal TotalSinIva { get; set; }
        public long IdDatosUsuario { get; set; }

        public virtual DatosUsuario IdDatosUsuarioNavigation { get; set; } = null!;
        public virtual Direccione IdDireccionNavigation { get; set; } = null!;
        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual FormasDeEntrega IdFormaDeEntregaNavigation { get; set; } = null!;
        public virtual FormasDeEnvio IdFormaDeEnvioNavigation { get; set; } = null!;
        public virtual FormasDePago IdFormaDePagoNavigation { get; set; } = null!;
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<ProductosPedido> ProductosPedidos { get; set; }
    }
}
