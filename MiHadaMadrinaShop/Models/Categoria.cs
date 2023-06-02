using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

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

        [Display(Name = "Categoría")]
        public string Categoria1 { get; set; } = null!;

        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }

        [Display(Name = "Activo")]
        public bool EsActivo { get; set; }
    
        public virtual ICollection<Subcategoria> Subcategoria { get; set; }

        public virtual ICollection<Producto> IdProductos { get; set; }
    }
}
