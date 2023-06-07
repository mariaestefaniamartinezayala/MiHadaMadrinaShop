using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Drawing;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.Productos
{
    [Area("Admin")]
    [Authorize(Roles = "Admin,Editor")]
    public class ProductosController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductosController(MiHadaMadrinaHandMadeDBContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Admin/Productos
        public async Task<IActionResult> Index(string searchString, string currentFilter, int? pageNumber)
        {
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            int pageSize = 10;

            if (searchString != null)
            {
                return View(await PaginatedList<Producto>.CreateAsync(_context.Productos.Where(s => s.Nombre.Contains(searchString) || s.DescripcionCorta.Contains(searchString)), pageNumber ?? 1, pageSize));
            }
            else
            {
                return View(await PaginatedList<Producto>.CreateAsync(_context.Productos, pageNumber ?? 1, pageSize));
            }
        }

        // GET: Admin/Productos/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // GET: Admin/Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("IdProducto,DescripcionCorta,DescripcionLarga,FechaDeEntrada,Imagen,Nombre,PorcentajeDeDescuento,Precio,PrecioConDescuento,Stock,UrlProductoDigital")] Producto producto)
        public async Task<IActionResult> Create(Producto producto)//Quito el bind por que da fallo. al añadir otra propiedad al modelo
        {
            if (ModelState.IsValid)
            {
                // Asignamos la fecha de entrada como el momento actual
                producto.FechaDeEntrada = DateTime.Now;

                // Calculamos el precio con descuento si hay un porcentaje de descuento válido
                if (producto.PorcentajeDeDescuento.HasValue)
                {
                    decimal descuento = producto.Precio * (decimal)(producto.PorcentajeDeDescuento.Value / 100.0);
                    producto.PrecioConDescuento = producto.Precio - descuento;
                }
                else
                {   // De lo contrario asignamos 0
                    producto.PorcentajeDeDescuento = 0;
                }

                // Obtenemos el nombre la imagen
                string nombresImagen = CargarImagenes(producto.ImagenFile);

                // Asignar la imagen principal
                //producto.ImagenPrincipalUrl = producto.ImagenFiles.FirstOrDefault()?.FileName;

                //Asignamos la url de las imágenes
                producto.ImagenUrl = nombresImagen;


                // Guardamos el producto en la base de datos
                _context.Add(producto);
                await _context.SaveChangesAsync();

                // Si no se ha seleccionado una imagen principal, se establece la primera imagen de la lista

                Producto p = _context.Productos.Where(q => q.IdProducto.Equals(producto.IdProducto)).FirstOrDefault();

                if (string.IsNullOrEmpty(p.ImagenPrincipalUrl) && p.ImagenUrl != null)
                {
                    p.ImagenPrincipalUrl = p.ImagenUrl.Split(',')[0];
                }




                var listaCategorias = _context.Categorias.ToList();

                foreach (var categoria in listaCategorias)
                {
                    if (Request.Form.Any(q => q.Key.Equals("CategoriaName " + categoria.IdCategoria)))
                    {
                        p.IdCategoria.Add(categoria);
                    }

                }



                // Guardamos el producto en la base de datos
                _context.Productos.Update(p);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Admin/Productos/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }


            return View(producto);
        }

        // POST: Admin/Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(long id, [Bind("IdProducto,DescripcionCorta,DescripcionLarga,FechaDeEntrada,Imagen,Nombre,PorcentajeDeDescuento,Precio,PrecioConDescuento,Stock,UrlProductoDigital")] Producto producto)
        public async Task<IActionResult> Edit(long id, Producto producto) //Elimino el bind, porque da fallos
        {
            if (ModelState.IsValid)
            {
                if(producto.ImagenFile != null)
                {
                    string nombresImagen = CargarImagenes(producto.ImagenFile);
                    producto.ImagenUrl = nombresImagen;
                    producto.ImagenPrincipalUrl = nombresImagen;
                }

                _context.Update(producto);


                var productoUpdate = _context.Productos.Include(a => a.IdCategoria).Where(q => q.IdProducto.Equals(id)).FirstOrDefault();
                productoUpdate.IdCategoria.Clear();

                var listaCategorias = _context.Categorias.ToList();

                foreach (var categoria in listaCategorias)
                {
                    if(Request.Form.Any(q=>q.Key.Equals("CategoriaName " + categoria.IdCategoria)))
                    {
                        productoUpdate.IdCategoria.Add(categoria);
                    }

                }


                _context.Update(productoUpdate);
                await _context.SaveChangesAsync();

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Productos/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Productos == null)
            {
                return NotFound();
            }

            var producto = await _context.Productos
                .FirstOrDefaultAsync(m => m.IdProducto == id);
            if (producto == null)
            {
                return NotFound();
            }

            return View(producto);
        }

        // POST: Admin/Productos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Productos == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.Productos'  is null.");
            }
            var producto = await _context.Productos.FindAsync(id);
            if (producto != null)
            {
                _context.Productos.Remove(producto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductoExists(long id)
        {
            return (_context.Productos?.Any(e => e.IdProducto == id)).GetValueOrDefault();
        }

        private string CargarImagenes(IFormFile imagen)
        {
            string nombreImagen = string.Empty;

            if (imagen != null)
            {
                // Ruta de la carpeta donde se guardarán las imágenes
                string carpetaImagenes = Path.Combine(_webHostEnvironment.WebRootPath, "img/productos");
                
                // Generar un nombre único para la imagen
                 nombreImagen = $"{Guid.NewGuid()}_{imagen.FileName}";
                
                // Ruta completa del archivo donde se guardará la imagen
                string rutaImagen = Path.Combine(carpetaImagenes, nombreImagen);
                
                // Crea la carpeta "img" si no existe
                Directory.CreateDirectory(carpetaImagenes);
                
                // Crear un FileStream para escribir el archivo en la ubicación especificada
                using (var fileStream = new FileStream(rutaImagen, FileMode.Create))
                {
                    // Copiar el contenido del archivo al FileStream
                    imagen.CopyTo(fileStream);
                }

            }

            return nombreImagen;
        }


        //private string CargarImagenes(List<IFormFile> imagenes)
        //{
        //    string nombresImagenes = string.Empty;

        //    if (imagenes != null && imagenes.Count > 0)
        //    {
        //        // Ruta de la carpeta donde se guardarán las imágenes
        //        string carpetaImagenes = Path.Combine(_webHostEnvironment.WebRootPath, "img");

        //        // Lista para almacenar los nombres de las imágenes
        //        var nombres = new List<string>();

        //        foreach (var imagen in imagenes)
        //        {
        //            // Generar un nombre único para la imagen
        //            string nombreImagen = $"{Guid.NewGuid()}_{imagen.FileName}";

        //            // Ruta completa del archivo donde se guardará la imagen
        //            string rutaImagen = Path.Combine(carpetaImagenes, nombreImagen);

        //            // Crea la carpeta "img" si no existe
        //            Directory.CreateDirectory(carpetaImagenes);

        //            // Crear un FileStream para escribir el archivo en la ubicación especificada
        //            using (var fileStream = new FileStream(rutaImagen, FileMode.Create))
        //            {
        //                // Copiar el contenido del archivo al FileStream
        //                imagen.CopyTo(fileStream);
        //            }

        //            // Agregar el nombre de la imagen a la lista de nombres
        //            nombres.Add(nombreImagen);
        //        }

        //        // Concatenar los nombres de las imágenes separados por comas
        //        nombresImagenes = string.Join(",", nombres);
        //    }

        //    return nombresImagenes;
        //}
    }
}
//[HttpPost]
//private string CargarImagen(Producto producto)
//{
//    string nombreImagen = string.Empty;

//    if (producto != null)
//    {
//        // Ruta de la carpeta donde se guardarán las imágenes
//        string carpetaImagenes = Path.Combine(_webHostEnvironment.WebRootPath, "img");

//        // Generar un nombre único para la imagen
//        nombreImagen = $"{Guid.NewGuid()}_{producto.ImagenFile.FileName}";

//        // Ruta completa del archivo donde se guardará la imagen
//        string rutaImagen = Path.Combine(carpetaImagenes, nombreImagen);

//        // Crea la carpeta "img" si no existe
//        Directory.CreateDirectory(carpetaImagenes);

//        // Crear un FileStream para escribir el archivo en la ubicación especificada
//        using (var fileStream = new FileStream(rutaImagen, FileMode.Create))
//        {
//            // Copiar el contenido del archivo ImagenFile del producto al FileStream
//            producto.ImagenFile.CopyTo(fileStream);
//        }
//    }

//    // Devolver el nombre de la imagen
//    return nombreImagen;
//}