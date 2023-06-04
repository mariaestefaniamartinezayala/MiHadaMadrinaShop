using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.FormasDeEntregas
{
    [Area("Admin")]
    public class FormasDeEntregasController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public FormasDeEntregasController(MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        // GET: Admin/FormasDeEntregas
        public async Task<IActionResult> Index()
        {
            return _context.FormasDeEntregas != null ?
                        View(await _context.FormasDeEntregas.ToListAsync()) :
                        Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.FormasDeEntregas'  is null.");
        }

        // GET: Admin/FormasDeEntregas/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.FormasDeEntregas == null)
            {
                return NotFound();
            }

            var formasDeEntrega = await _context.FormasDeEntregas
                .FirstOrDefaultAsync(m => m.IdFormaDeEntrega == id);
            if (formasDeEntrega == null)
            {
                return NotFound();
            }

            return View(formasDeEntrega);
        }

        // GET: Admin/FormasDeEntregas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/FormasDeEntregas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormaDeEntrega,FormaDeEntrega")] FormasDeEntrega formasDeEntrega)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formasDeEntrega);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formasDeEntrega);
        }

        // GET: Admin/FormasDeEntregas/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.FormasDeEntregas == null)
            {
                return NotFound();
            }

            var formasDeEntrega = await _context.FormasDeEntregas.FindAsync(id);
            if (formasDeEntrega == null)
            {
                return NotFound();
            }
            return View(formasDeEntrega);
        }

        // POST: Admin/FormasDeEntregas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdFormaDeEntrega,FormaDeEntrega")] FormasDeEntrega formasDeEntrega)
        {
            if (id != formasDeEntrega.IdFormaDeEntrega)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formasDeEntrega);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormasDeEntregaExists(formasDeEntrega.IdFormaDeEntrega))
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
            return View(formasDeEntrega);
        }

        // GET: Admin/FormasDeEntregas/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.FormasDeEntregas == null)
            {
                return NotFound();
            }

            var formasDeEntrega = await _context.FormasDeEntregas
                .FirstOrDefaultAsync(m => m.IdFormaDeEntrega == id);
            if (formasDeEntrega == null)
            {
                return NotFound();
            }

            return View(formasDeEntrega);
        }

        // POST: Admin/FormasDeEntregas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.FormasDeEntregas == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.FormasDeEntregas'  is null.");
            }
            var formasDeEntrega = await _context.FormasDeEntregas.FindAsync(id);
            if (formasDeEntrega != null)
            {
                _context.FormasDeEntregas.Remove(formasDeEntrega);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormasDeEntregaExists(byte id)
        {
            return (_context.FormasDeEntregas?.Any(e => e.IdFormaDeEntrega == id)).GetValueOrDefault();
        }
    }
}
