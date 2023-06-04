using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.FormasDeEnvios
{
    [Area("Admin")]
    public class FormasDeEnviosController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public FormasDeEnviosController(MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        // GET: Admin/FormasDeEnvios
        public async Task<IActionResult> Index()
        {
            return _context.FormasDeEnvios != null ?
                        View(await _context.FormasDeEnvios.ToListAsync()) :
                        Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.FormasDeEnvios'  is null.");
        }

        // GET: Admin/FormasDeEnvios/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.FormasDeEnvios == null)
            {
                return NotFound();
            }

            var formasDeEnvio = await _context.FormasDeEnvios
                .FirstOrDefaultAsync(m => m.IdFormaDeEnvio == id);
            if (formasDeEnvio == null)
            {
                return NotFound();
            }

            return View(formasDeEnvio);
        }

        // GET: Admin/FormasDeEnvios/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/FormasDeEnvios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdFormaDeEnvio,FormaDeEnvio")] FormasDeEnvio formasDeEnvio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(formasDeEnvio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(formasDeEnvio);
        }

        // GET: Admin/FormasDeEnvios/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.FormasDeEnvios == null)
            {
                return NotFound();
            }

            var formasDeEnvio = await _context.FormasDeEnvios.FindAsync(id);
            if (formasDeEnvio == null)
            {
                return NotFound();
            }
            return View(formasDeEnvio);
        }

        // POST: Admin/FormasDeEnvios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdFormaDeEnvio,FormaDeEnvio")] FormasDeEnvio formasDeEnvio)
        {
            if (id != formasDeEnvio.IdFormaDeEnvio)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(formasDeEnvio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FormasDeEnvioExists(formasDeEnvio.IdFormaDeEnvio))
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
            return View(formasDeEnvio);
        }

        // GET: Admin/FormasDeEnvios/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.FormasDeEnvios == null)
            {
                return NotFound();
            }

            var formasDeEnvio = await _context.FormasDeEnvios
                .FirstOrDefaultAsync(m => m.IdFormaDeEnvio == id);
            if (formasDeEnvio == null)
            {
                return NotFound();
            }

            return View(formasDeEnvio);
        }

        // POST: Admin/FormasDeEnvios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.FormasDeEnvios == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.FormasDeEnvios'  is null.");
            }
            var formasDeEnvio = await _context.FormasDeEnvios.FindAsync(id);
            if (formasDeEnvio != null)
            {
                _context.FormasDeEnvios.Remove(formasDeEnvio);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FormasDeEnvioExists(byte id)
        {
            return (_context.FormasDeEnvios?.Any(e => e.IdFormaDeEnvio == id)).GetValueOrDefault();
        }
    }
}
