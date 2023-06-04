using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiHadaMadrinaShop.Models
{
    public partial class Pedido
    {
        public Pedido()
        {
            Facturas = new HashSet<Factura>();
            ProductosPedidos = new HashSet<ProductosPedido>();
            TCesta = new HashSet<TCestum>();
        }

        public long IdPedido { get; set; }
        public long IdDireccion { get; set; }
        public int IdEstado { get; set; }
        public byte IdFormaDeEntrega { get; set; }
        public byte IdFormaDeEnvio { get; set; }
        public byte IdFormaDePago { get; set; }
        public decimal Iva { get; set; }

        [Display(Name = "Porcentaje de descuento")]
        public byte? PorcentajeDescuento { get; set; }
        public decimal Total { get; set; }

        [Display(Name = "Total sin IVA")]
        public decimal TotalSinIva { get; set; }
        public string IdAspNetUsers { get; set; } = null!;

        [Display(Name = "Fecha de pedido")]
        [DataType(DataType.Date)]
        public DateTime FechaPedido { get; set; }

        [Display(Name = "Fecha de envío")]
        [DataType(DataType.Date)]
        public DateTime FechaEnvio { get; set; }

        public virtual AspNetUser IdAspNetUsersNavigation { get; set; } = null!;
        public virtual Direccione IdDireccionNavigation { get; set; } = null!;
        public virtual Estado IdEstadoNavigation { get; set; } = null!;
        public virtual FormasDeEntrega IdFormaDeEntregaNavigation { get; set; } = null!;
        public virtual FormasDeEnvio IdFormaDeEnvioNavigation { get; set; } = null!;
        public virtual FormasDePago IdFormaDePagoNavigation { get; set; } = null!;
        public virtual ICollection<Factura> Facturas { get; set; }
        public virtual ICollection<ProductosPedido> ProductosPedidos { get; set; }
        public virtual ICollection<TCestum> TCesta { get; set; }
    }
}
