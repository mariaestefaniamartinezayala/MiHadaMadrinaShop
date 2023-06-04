using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.FormasDePagos
{
    [Area("Admin")]
    public class FormasDePagosController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public FormasDePagosController(MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        // GET: Admin/FormasDePagoes
        public async Task<IActionResult> Index()
        {
            return _context.FormasDePagos != null ?
                        View(await _context.FormasDePagos.ToListAsync()) :
                        Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.FormasDePagos'  is null.");
        }

        // GET: Admin/FormasDePagoes/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.FormasDePagos == null)
            {
                return NotFound();
            }

            var formasDePago = await _context.FormasDePagos
                .FirstOrDefaultAsync(m => m.IdFormaDePago == id);
            if (formasDePago == null)
            {
                return NotFound();
            }

            return View(formasDePago);
        }

        // GET: Admin/FormasDePagoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/FormasDePagoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormaDePago,FormaDePago")] FormasDePago formasDePago)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formasDePago);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formasDePago);
        }

        // GET: Admin/FormasDePagoes/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.FormasDePagos == null)
            {
                return NotFound();
            }

            var formasDePago = await _context.FormasDePagos.FindAsync(id);
            if (formasDePago == null)
            {
                return NotFound();
            }
            return View(formasDePago);
        }

        // POST: Admin/FormasDePagoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdFormaDePago,FormaDePago")] FormasDePago formasDePago)
        {
            if (id != formasDePago.IdFormaDePago)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formasDePago);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormasDePagoExists(formasDePago.IdFormaDePago))
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
            return View(formasDePago);
        }

        // GET: Admin/FormasDePagoes/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.FormasDePagos == null)
            {
                return NotFound();
            }

            var formasDePago = await _context.FormasDePagos
                .FirstOrDefaultAsync(m => m.IdFormaDePago == id);
            if (formasDePago == null)
            {
                return NotFound();
            }

            return View(formasDePago);
        }

        // POST: Admin/FormasDePagoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.FormasDePagos == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.FormasDePagos'  is null.");
            }
            var formasDePago = await _context.FormasDePagos.FindAsync(id);
            if (formasDePago != null)
            {
                _context.FormasDePagos.Remove(formasDePago);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormasDePagoExists(byte id)
        {
            return (_context.FormasDePagos?.Any(e => e.IdFormaDePago == id)).GetValueOrDefault();
        }
    }
}
