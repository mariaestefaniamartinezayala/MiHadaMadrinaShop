//using system;
//using system.collections.generic;
//using system.componentmodel.dataannotations;
//using system.componentmodel.dataannotations.schema;
//using microsoft.aspnetcore.http;

//namespace mihadamadrinashop.models
//{
//    public partial class producto
//    {
//        public producto()
//        {
//            imagenfiles = new list<iformfile>();
//            productospedidos = new hashset<productospedido>();
//            tcesta = new hashset<tcestum>();
//            idcategoria = new hashset<categoria>();
//        }

//        [display(name = "id")]
//        public long idproducto { get; set; }

//        [display(name = "descripción corta")]
//        public string? descripcioncorta { get; set; }

//        [display(name = "descripción larga")]
//        public string? descripcionlarga { get; set; }

//        [display(name = "fecha de entrada")]
//        public datetime fechadeentrada { get; set; }

//        [display(name = "imagen")]
//        public string? imagenurl { get; set; }

//        [notmapped]
//        [display(name = "seleccionar imagenes")]
//        public iformfile? imagenfile { get; set; }

//        [notmapped]
//        [required(errormessage = "debes seleccionar al menos una imagen")]
//        public list<iformfile> imagenfiles { get; set; }

//        [display(name = "imagen principal")]
//        public string? imagenprincipalurl { get; set; }

//        [display(name = "nombre")]
//        public string nombre { get; set; } = null!;

//        [display(name = "porcentaje de descuento")]
//        public int? porcentajededescuento { get; set; }

//        public decimal precio { get; set; }

//        [display(name = "precio con descuento")]
//        public decimal? preciocondescuento { get; set; }

//        public int? stock { get; set; }

//        [display(name = "url de producto digital")]
//        public string? urlproductodigital { get; set; }

//        [display(name = "es digital")]
//        public bool esdigital { get; set; }

//        [display(name = "activo")]
//        public bool esactivo { get; set; }

//        public virtual icollection<productospedido> productospedidos { get; set; }
//        public virtual icollection<tcestum> tcesta { get; set; }
//        public virtual icollection<categoria> idcategoria { get; set; }
//    }
//}
