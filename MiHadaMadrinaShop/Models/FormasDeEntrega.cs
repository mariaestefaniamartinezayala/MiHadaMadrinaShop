using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace MiHadaMadrinaShop.Models
{
    public partial class FormasDeEntrega
    {
        public FormasDeEntrega()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public byte IdFormaDeEntrega { get; set; }

        [Display(Name = "Forma de entrega")]
        public string FormaDeEntrega { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
