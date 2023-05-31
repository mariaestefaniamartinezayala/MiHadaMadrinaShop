//using System;
//using System.Collections.Generic;
//using System.ComponentModel.DataAnnotations;

//namespace MiHadaMadrinaShop.Models
//{
//    public partial class Categoria
//    {
//        public Categoria()
//        {
//            Subcategoria = new HashSet<Subcategoria>();
//            IdProductos = new HashSet<Producto>();
//        }

//        public int IdCategoria { get; set; }

//        [Display(Name = "Categoría")]
//        public string? Categoria1 { get; set; }

//        [Display(Name = "Descripción")]
//        public string? Descripcion { get; set; }

//        [Display(Name = "Activo")]
//        public bool EsActivo { get; set; }

//        public virtual ICollection<Subcategoria> Subcategoria { get; set; }

//        public virtual ICollection<Producto> IdProductos { get; set; }
//    }
//}
