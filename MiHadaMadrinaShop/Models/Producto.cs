using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace MiHadaMadrinaShop.Models
{
    public partial class Producto
    {
        public Producto()
        {
            //ImagenFiles = new List<IFormFile>();
            ProductosPedidos = new HashSet<ProductosPedido>();
            TCesta = new HashSet<TCestum>();
            IdCategoria = new HashSet<Categoria>();
        }

        [Display(Name = "ID")]
        public long IdProducto { get; set; }

        [Display(Name = "Descripción corta")]
        public string? DescripcionCorta { get; set; }

        [Display(Name = "Descripción larga")]
        public string? DescripcionLarga { get; set; }

        [Display(Name = "Fecha de entrada")]
        public DateTime? FechaDeEntrada { get; set; }

        [Display(Name = "Imagen")]
        public string? ImagenUrl { get; set; }

        [NotMapped]
        [Display(Name = "Seleccionar imagenes")]
        public IFormFile? ImagenFile { get; set; }

    
        [Display(Name = "Imagen principal")]
        public string? ImagenPrincipalUrl { get; set; }

        [Display(Name = "Nombre")]
        public string Nombre { get; set; } = null!;

        [Display(Name = "Porcentaje de descuento")]
        public int? PorcentajeDeDescuento { get; set; }

        public decimal Precio { get; set; }

        [Display(Name = "Precio con descuento")]
        public decimal? PrecioConDescuento { get; set; }

        public int? Stock { get; set; }

        [Display(Name = "URL de producto digital")]
        public string? UrlProductoDigital { get; set; }

        [Display(Name = "Es digital")]
        public bool EsDigital { get; set; }

        [Display(Name = "Activo")]
        public bool EsActivo { get; set; }

        public virtual ICollection<ProductosPedido> ProductosPedidos { get; set; }
        public virtual ICollection<TCestum> TCesta { get; set; }
        public virtual ICollection<Categoria> IdCategoria { get; set; }
    }
}
