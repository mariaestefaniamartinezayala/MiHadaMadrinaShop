using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.Pedidos
{
    [Area("Admin")]
    public class PedidosController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public PedidosController(MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Pedidoes
        public async Task<IActionResult> Index()
        {
            var miHadaMadrinaHandMadeDBContext = _context.Pedidos;
            return View(await miHadaMadrinaHandMadeDBContext.ToListAsync());
        }

        // GET: Admin/Pedidoes/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .FirstOrDefaultAsync(m => m.IdPedido == id);

            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // GET: Admin/Pedidoes/Create
        public IActionResult Create()
        {
            ViewData["IdAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado");
            ViewData["IdFormaDeEntrega"] = new SelectList(_context.FormasDeEntregas, "IdFormaDeEntrega", "IdFormaDeEntrega");
            ViewData["IdFormaDeEnvio"] = new SelectList(_context.FormasDeEnvios, "IdFormaDeEnvio", "IdFormaDeEnvio");
            ViewData["IdFormaDePago"] = new SelectList(_context.FormasDePagos, "IdFormaDePago", "IdFormaDePago");
            return View();
        }

        // POST: Admin/Pedidoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Pedido pedido)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pedido);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id", pedido.IdAspNetUsers);
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado", pedido.IdEstado);
            ViewData["IdFormaDeEntrega"] = new SelectList(_context.FormasDeEntregas, "IdFormaDeEntrega", "IdFormaDeEntrega", pedido.IdFormaDeEntrega);
            ViewData["IdFormaDeEnvio"] = new SelectList(_context.FormasDeEnvios, "IdFormaDeEnvio", "IdFormaDeEnvio", pedido.IdFormaDeEnvio);
            ViewData["IdFormaDePago"] = new SelectList(_context.FormasDePagos, "IdFormaDePago", "IdFormaDePago", pedido.IdFormaDePago);
            return View(pedido);
        }

        // GET: Admin/Pedidoes/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido == null)
            {
                return NotFound();
            }
            ViewData["IdAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id", pedido.IdAspNetUsers);
            ViewData["IdEstado"] = new SelectList(_context.Estados, "IdEstado", "IdEstado", pedido.IdEstado);
            ViewData["IdFormaDeEntrega"] = new SelectList(_context.FormasDeEntregas, "IdFormaDeEntrega", "IdFormaDeEntrega", pedido.IdFormaDeEntrega);
            ViewData["IdFormaDeEnvio"] = new SelectList(_context.FormasDeEnvios, "IdFormaDeEnvio", "IdFormaDeEnvio", pedido.IdFormaDeEnvio);
            ViewData["IdFormaDePago"] = new SelectList(_context.FormasDePagos, "IdFormaDePago", "IdFormaDePago", pedido.IdFormaDePago);
            return View(pedido);
        }

        // POST: Admin/Pedidoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, Pedido pedido)
        {
            if (id != pedido.IdPedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
               
                    _context.Update(pedido);
                    await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));

            }
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Pedidoes/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos
                .Include(p => p.IdAspNetUsersNavigation)
                .Include(p => p.IdEstadoNavigation)
                .Include(p => p.IdFormaDeEntregaNavigation)
                .Include(p => p.IdFormaDeEnvioNavigation)
                .Include(p => p.IdFormaDePagoNavigation)
                .FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // POST: Admin/Pedidoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            if (_context.Pedidos == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.Pedidos'  is null.");
            }
            var pedido = await _context.Pedidos.FindAsync(id);
            if (pedido != null)
            {
                _context.Pedidos.Remove(pedido);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PedidoExists(long id)
        {
            return (_context.Pedidos?.Any(e => e.IdPedido == id)).GetValueOrDefault();
        }
    }
}
