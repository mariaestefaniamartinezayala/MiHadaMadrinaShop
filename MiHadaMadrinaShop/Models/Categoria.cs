using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            Subcategoria = new HashSet<Subcategoria>();
            IdProductos = new HashSet<Producto>();
        }

        public int IdCategoria { get; set; }
        public string Categoria1 { get; set; } = null!;
        public string? Descripcion { get; set; }

        public virtual ICollection<Subcategoria> Subcategoria { get; set; }

        public virtual ICollection<Producto> IdProductos { get; set; }
    }
}
