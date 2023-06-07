using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Identity.Pages.Account.Manage.Direcciones
{
    public class DeleteModel : PageModel
    {
        private readonly MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext _context;

        public DeleteModel(MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Direccione Direccione { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Direcciones == null)
            {
                return NotFound();
            }

            var direccione = await _context.Direcciones.FirstOrDefaultAsync(m => m.IdDireccion == id);

            if (direccione == null)
            {
                return NotFound();
            }
            else 
            {
                Direccione = direccione;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(long? id)
        {
            if (id == null || _context.Direcciones == null)
            {
                return NotFound();
            }
            var direccione = await _context.Direcciones.FindAsync(id);

            if (direccione != null)
            {
                Direccione = direccione;
                _context.Direcciones.Remove(Direccione);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./IndexDirecciones");
        }
    }
}
