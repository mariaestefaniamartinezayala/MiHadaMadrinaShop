using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Subcategoria
    {
        public int IdSubcategoria { get; set; }
        public int IdCategoria { get; set; }
        public string? Descripcion { get; set; }
        public string Subcategoria1 { get; set; } = null!;

        public virtual Categoria IdCategoriaNavigation { get; set; } = null!;
    }
}
