using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MiHadaMadrinaShop.Models
{
    public partial class FormasDeEnvio
    {
        public FormasDeEnvio()
        {
            Pedidos = new HashSet<Pedido>();
        }

        public byte IdFormaDeEnvio { get; set; }

        [Display(Name = "Forma de envío")]
        public string FormaDeEnvio { get; set; } = null!;

        public virtual ICollection<Pedido> Pedidos { get; set; }
    }
}
