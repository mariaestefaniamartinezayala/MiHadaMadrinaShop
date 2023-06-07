using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Public.Controllers.Productos
{
    [Area("Public")]
    public class ProductosController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public ProductosController(MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        // GET: Public/Productos
        public async Task<IActionResult> Index(int? idCategoria, string searchString, string currentFilter, int? pageNumber)
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

            if(idCategoria != null)
            {
                if (searchString != null)
                {
                    return View(await PaginatedList<Producto>.CreateAsync(_context.Productos.Include(q => q.IdCategoria).Where(p => p.IdCategoria.Any(q => q.IdCategoria.Equals(idCategoria))).Where(s => s.Nombre.Contains(searchString) || s.DescripcionCorta.Contains(searchString)), pageNumber ?? 1, pageSize));
                }
                else
                {
                    return View(await PaginatedList<Producto>.CreateAsync(_context.Productos.Include(q => q.IdCategoria).Where(p => p.IdCategoria.Any(q => q.IdCategoria.Equals(idCategoria))), pageNumber ?? 1, pageSize));
                }
            }
            else
            {
                if (searchString != null)
                {
                    return View(await PaginatedList<Producto>.CreateAsync(_context.Productos.Where(s => s.Nombre.Contains(searchString) || s.DescripcionCorta.Contains(searchString)), pageNumber ?? 1, pageSize));
                }
                else
                {
                    return View(await PaginatedList<Producto>.CreateAsync(_context.Productos, pageNumber ?? 1, pageSize));
                }
            }


        }

        public async Task<IActionResult> IndexPorCategoria(string searchString, string currentFilter, int? pageNumber, int idCategoria)
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
                return View(await PaginatedList<Producto>.CreateAsync(_context.Productos.Include(q => q.IdCategoria).Where(p => p.IdCategoria.Any(q => q.IdCategoria.Equals(idCategoria))).Where(s => s.Nombre.Contains(searchString) || s.DescripcionCorta.Contains(searchString)), pageNumber ?? 1, pageSize));
            }
            else
            {
                return View(await PaginatedList<Producto>.CreateAsync(_context.Productos.Include(q => q.IdCategoria).Where(p => p.IdCategoria.Any(q => q.IdCategoria.Equals(idCategoria))), pageNumber ?? 1, pageSize));
            }

        }

        [HttpGet]
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

        // GET: Public/Productos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Public/Productos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdProducto,DescripcionCorta,DescripcionLarga,FechaDeEntrada,Imagen,Nombre,PorcentajeDeDescuento,Precio,PrecioConDescuento,Stock,UrlProductoDigital")] Producto producto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(producto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(producto);
        }

        // GET: Public/Productos/Edit/5
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

        // POST: Public/Productos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdProducto,DescripcionCorta,DescripcionLarga,FechaDeEntrada,Imagen,Nombre,PorcentajeDeDescuento,Precio,PrecioConDescuento,Stock,UrlProductoDigital")] Producto producto)
        {
            if (id != producto.IdProducto)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(producto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductoExists(producto.IdProducto))
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
            return View(producto);
        }

        // GET: Public/Productos/Delete/5
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

        // POST: Public/Productos/Delete/5
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
    }
}
