using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Public.Controllers.TCestums
{
    [Area("Public")]
    public class TCestumsController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public TCestumsController(MiHadaMadrinaHandMadeDBContext context)
        {

            _context = context;
        }

        // GET: Public/TCestums
        public async Task<IActionResult> Index()
        {
            var contextListaCestas = _context.TCesta.Where(q => q.IdAppNetUsers.Equals(User.Identity.GetUserId())).ToList();
            var contextProductos = _context.Productos;

            List<TCestum> listaCestas = new List<TCestum>();

            decimal totalCesta = 0;

            foreach (TCestum? cesta in contextListaCestas)
            {
                Producto producto = contextProductos.Where(q => q.IdProducto.Equals(cesta.IdProducto)).FirstOrDefault();
                cesta.NombreProducto = producto.Nombre;
                cesta.PrecioProducto = producto.PrecioConDescuento;
                cesta.DescuentoProducto = producto.PorcentajeDeDescuento;
                cesta.ImagenUrl = producto.ImagenUrl;
                cesta.StockProducto = producto.Stock;
                totalCesta = Math.Round((decimal)(totalCesta + cesta.PrecioProducto * cesta.Cantidad * (decimal)1.21),2);


                listaCestas.Add(cesta);

            }

            foreach (TCestum? cesta in contextListaCestas)
            {
                cesta.TotalCesta = totalCesta;
            }


            return View(listaCestas);
        }

        // GET: Public/TCestums/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.TCesta == null)
            {
                return NotFound();
            }

            var tCestum = await _context.TCesta
                .Include(t => t.IdAppNetUsersNavigation)
                .FirstOrDefaultAsync(m => m.IdCesta == id);
            if (tCestum == null)
            {
                return NotFound();
            }

            return View(tCestum);
        }

        // GET: Public/TCestums/Create
        public IActionResult Create()
        {
            ViewData["IdDatosUsuario"] = new SelectList(_context.AspNetUsers, "IdDatosUsuario", "IdDatosUsuario");
            return View();
        }

        // POST: Public/TCestums/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdCesta,Cantidad,IdProducto,Iva,PorcentajeDeDescuento,Total,TotalSinIva,IdDatosUsuario")] TCestum tCestum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tCestum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDatosUsuario"] = new SelectList(_context.AspNetUsers, "IdDatosUsuario", "IdDatosUsuario", tCestum.IdDatosUsuario);
            return View(tCestum);
        }

        // GET: Public/TCestums/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.TCesta == null)
            {
                return NotFound();
            }

            var tCestum = await _context.TCesta.FindAsync(id);
            if (tCestum == null)
            {
                return NotFound();
            }
            ViewData["IdDatosUsuario"] = new SelectList(_context.AspNetUsers, "IdDatosUsuario", "IdDatosUsuario", tCestum.IdDatosUsuario);
            return View(tCestum);
        }

        // POST: Public/TCestums/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdCesta,Cantidad,IdProducto,Iva,PorcentajeDeDescuento,Total,TotalSinIva,IdDatosUsuario")] TCestum tCestum)
        {
            if (id != tCestum.IdCesta)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tCestum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TCestumExists(tCestum.IdCesta))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDatosUsuario"] = new SelectList(_context.AspNetUsers, "IdDatosUsuario", "IdDatosUsuario", tCestum.IdDatosUsuario);
            return View(tCestum);
        }

        // GET: Public/TCestums/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.TCesta == null)
            {
                return NotFound();
            }

            var tCestum = await _context.TCesta
                .Include(t => t.IdAppNetUsersNavigation)
                .FirstOrDefaultAsync(m => m.IdCesta == id);
            if (tCestum == null)
            {
                return NotFound();
            }

            return View(tCestum);
        }

        // POST: Public/TCestums/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.TCesta == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.TCesta'  is null.");
            }
            var tCestum = await _context.TCesta.FindAsync(id);
            if (tCestum != null)
            {
                _context.TCesta.Remove(tCestum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TCestumExists(long id)
        {
            return (_context.TCesta?.Any(e => e.IdCesta == id)).GetValueOrDefault();
        }


        public async Task<IActionResult> AddProductToCart(long id)
        {

            var idUser = User.Identity.GetUserId();



            TCestum tCestum = new TCestum();

            Producto producto = _context.Productos.Where(q => q.IdProducto.Equals(id)).FirstOrDefault();

            //SI HAY ALGUNA CESTA(LINEA EN LA BBDD) DE ESE USUARIO Y PRODUCTO SOLO ACTUALIZAR CANTIDAD
            if (_context.TCesta.Any(q => q.IdProducto.Equals(producto.IdProducto) && q.IdAppNetUsers.Equals(idUser)))
            {
                tCestum = _context.TCesta.Where(q => q.IdProducto.Equals(producto.IdProducto) && q.IdAppNetUsers.Equals(idUser)).FirstOrDefault();
                tCestum.Cantidad = tCestum.Cantidad + 1;
                tCestum.TotalSinIva = tCestum.TotalSinIva + producto.PrecioConDescuento;
                tCestum.Total = tCestum.TotalSinIva * (decimal)1.21;

                _context.TCesta.Update(tCestum);
            }
            //SI NO HAY NINGUNA CESTA(LINEA EN LA BBDD) SE CREA UNA NUEVA DE ESE USUARIO 
            else if (!_context.TCesta.Any())
            {
                _context.Add(tCestum);

                //tCestum.IdCesta = 1;
                tCestum.IdAppNetUsers = idUser;
                tCestum.IdProducto = producto.IdProducto;
                tCestum.Cantidad = 1;
                tCestum.Iva = 21;
                tCestum.TotalSinIva = 0;
                tCestum.TotalSinIva = tCestum.TotalSinIva + producto.PrecioConDescuento;
                tCestum.Total = tCestum.TotalSinIva * (decimal)1.21;

                _context.TCesta.Add(tCestum);

            }
            //SI HAY ALGUNA CESTA PERO NO DE ESE USUARIO Y PRODUCTO SE CREA UNA NUEVA
            else
            {
                _context.Add(tCestum);

                //tCestum.IdCesta = _context.TCesta.OrderByDescending(q => q.IdCesta).FirstOrDefault().IdCesta + 1;
                tCestum.IdProducto = producto.IdProducto;
                tCestum.IdAppNetUsers = idUser;
                tCestum.Cantidad = 1;
                tCestum.Iva = 21;
                tCestum.TotalSinIva = 0;
                tCestum.TotalSinIva = tCestum.TotalSinIva + producto.PrecioConDescuento;
                tCestum.Total = tCestum.TotalSinIva * (decimal)1.21;

                _context.TCesta.Add(tCestum);
            }


            await _context.SaveChangesAsync();



            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> ActualizarCesta([FromBody] TCestum data)
        {
            var cestaContextUsuario = _context.TCesta.Where(q => q.IdAppNetUsers.Equals(User.Identity.GetUserId()));

            

            foreach (TCestum? cestaUsuario in cestaContextUsuario)
            {
                Producto producto = _context.Productos.Where(q => q.IdProducto.Equals(cestaUsuario.IdProducto)).FirstOrDefault();
                cestaUsuario.NombreProducto = producto.Nombre;
                cestaUsuario.DescuentoProducto = producto.PorcentajeDeDescuento;
                cestaUsuario.StockProducto = producto.Stock;
                cestaUsuario.ImagenUrl = producto.ImagenUrl;


                int? cantidad = 0;

                if (cestaUsuario.IdCesta.Equals(data.IdCesta))
                {
                    cantidad = data.Cantidad;
                }
                else
                {
                    cantidad = cestaUsuario.Cantidad;
                }

                cestaUsuario.Cantidad = cantidad;
                cestaUsuario.TotalSinIva = cantidad * producto.PrecioConDescuento;
                cestaUsuario.Total = cestaUsuario.TotalSinIva * (decimal)1.21;
                cestaUsuario.TotalCesta = cestaUsuario.Total;
                _context.TCesta.Update(cestaUsuario);
            }

            decimal? totalCesta = 0;

            foreach (TCestum? cestaUsuario in cestaContextUsuario)
            {
                totalCesta = totalCesta + cestaUsuario.Total;
               
            }

            foreach (TCestum? cestaUsuario in cestaContextUsuario)
            {
                cestaUsuario.TotalCesta = totalCesta;

                _context.TCesta.Update(cestaUsuario);

            }

            await _context.SaveChangesAsync();
            return View();
            
        }

      
    }
}
