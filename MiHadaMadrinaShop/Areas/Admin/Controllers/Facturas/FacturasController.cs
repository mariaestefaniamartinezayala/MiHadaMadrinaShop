using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.Facturas
{
    [Area("Admin")]
    public class FacturasController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public FacturasController(MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Facturas
        public async Task<IActionResult> Index()
        {
            var miHadaMadrinaHandMadeDBContext = _context.Facturas.Include(f => f.IdPedidoNavigation);
            return View(await miHadaMadrinaHandMadeDBContext.ToListAsync());
        }

        // GET: Admin/Facturas/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // GET: Admin/Facturas/Create
        public IActionResult Create()
        {
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido");
            return View();
        }

        // POST: Admin/Facturas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFactura,IdPedido")] Factura factura)
        {
            if (ModelState.IsValid)
            {
                _context.Add(factura);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", factura.IdPedido);
            return View(factura);
        }

        // GET: Admin/Facturas/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas.FindAsync(id);
            if (factura == null)
            {
                return NotFound();
            }
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", factura.IdPedido);
            return View(factura);
        }

        // POST: Admin/Facturas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("IdFactura,IdPedido")] Factura factura)
        {
            if (id != factura.IdFactura)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(factura);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacturaExists(factura.IdFactura))
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
            ViewData["IdPedido"] = new SelectList(_context.Pedidos, "IdPedido", "IdPedido", factura.IdPedido);
            return View(factura);
        }

        // GET: Admin/Facturas/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Facturas == null)
            {
                return NotFound();
            }

            var factura = await _context.Facturas
                .Include(f => f.IdPedidoNavigation)
                .FirstOrDefaultAsync(m => m.IdFactura == id);
            if (factura == null)
            {
                return NotFound();
            }

            return View(factura);
        }

        // POST: Admin/Facturas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Facturas == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.Facturas'  is null.");
            }
            var factura = await _context.Facturas.FindAsync(id);
            if (factura != null)
            {
                _context.Facturas.Remove(factura);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FacturaExists(long id)
        {
            return (_context.Facturas?.Any(e => e.IdFactura == id)).GetValueOrDefault();
        }
    }
}
