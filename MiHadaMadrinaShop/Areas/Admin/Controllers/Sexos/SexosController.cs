using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Admin.Controllers.Sexos
{
    [Area("Admin")]
    public class SexosController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public SexosController(MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Sexos
        public async Task<IActionResult> Index()
        {
              return _context.Sexos != null ? 
                          View(await _context.Sexos.ToListAsync()) :
                          Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.Sexos'  is null.");
        }

        // GET: Admin/Sexos/Details/5
        public async Task<IActionResult> Details(byte? id)
        {
            if (id == null || _context.Sexos == null)
            {
                return NotFound();
            }

            var sexo = await _context.Sexos
                .FirstOrDefaultAsync(m => m.IdSexo == id);
            if (sexo == null)
            {
                return NotFound();
            }

            return View(sexo);
        }

        // GET: Admin/Sexos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Sexos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdSexo,Sexo1")] Sexo sexo)
        {
            if (ModelState.IsValid)
            {
                _context.Add(sexo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(sexo);
        }

        // GET: Admin/Sexos/Edit/5
        public async Task<IActionResult> Edit(byte? id)
        {
            if (id == null || _context.Sexos == null)
            {
                return NotFound();
            }

            var sexo = await _context.Sexos.FindAsync(id);
            if (sexo == null)
            {
                return NotFound();
            }
            return View(sexo);
        }

        // POST: Admin/Sexos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(byte id, [Bind("IdSexo,Sexo1")] Sexo sexo)
        {
            if (id != sexo.IdSexo)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(sexo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SexoExists(sexo.IdSexo))
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
            return View(sexo);
        }

        // GET: Admin/Sexos/Delete/5
        public async Task<IActionResult> Delete(byte? id)
        {
            if (id == null || _context.Sexos == null)
            {
                return NotFound();
            }

            var sexo = await _context.Sexos
                .FirstOrDefaultAsync(m => m.IdSexo == id);
            if (sexo == null)
            {
                return NotFound();
            }

            return View(sexo);
        }

        // POST: Admin/Sexos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(byte id)
        {
            if (_context.Sexos == null)
            {
                return Problem("Entity set 'MiHadaMadrinaHandMadeDBContext.Sexos'  is null.");
            }
            var sexo = await _context.Sexos.FindAsync(id);
            if (sexo != null)
            {
                _context.Sexos.Remove(sexo);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SexoExists(byte id)
        {
          return (_context.Sexos?.Any(e => e.IdSexo == id)).GetValueOrDefault();
        }
    }
}
