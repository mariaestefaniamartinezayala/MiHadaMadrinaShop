using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class DatosUsuario
    {
        public DatosUsuario()
        {
            Direcciones = new HashSet<Direccione>();
            Pedidos = new HashSet<Pedido>();
            TCesta = new HashSet<TCestum>();
        }

        public long IdDatosUsuario { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public bool? Sexo { get; set; }
        public long? Telefono { get; set; }
        public string IdUserAuth { get; set; } = null!;
        public byte IdSexo { get; set; }

        public virtual Sexo IdSexoNavigation { get; set; } = null!;
        public virtual AspNetUser IdUserAuthNavigation { get; set; } = null!;
        public virtual ICollection<Direccione> Direcciones { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<TCestum> TCesta { get; set; }
    }
}
