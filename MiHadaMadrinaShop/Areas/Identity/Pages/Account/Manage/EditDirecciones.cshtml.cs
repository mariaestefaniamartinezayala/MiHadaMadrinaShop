using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Identity.Pages.Account.Manage.Direcciones
{
    public class EditModel : PageModel
    {
        private readonly MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext _context;

        public EditModel(MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext context)
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

            var direccione =  await _context.Direcciones.FirstOrDefaultAsync(m => m.IdDireccion == id);
            if (direccione == null)
            {
                return NotFound();
            }
            Direccione = direccione;
           ViewData["IdAspNetUsers"] = new SelectList(_context.AspNetUsers, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //if (!ModelState.IsValid)
            //{
            //    return Page();
            //}
            var user = User.Identity.GetUserId();
            Direccione.IdAspNetUsers = user;

            _context.Attach(Direccione).State = EntityState.Modified;
           

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DireccioneExists(Direccione.IdDireccion))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./IndexDirecciones");
        }

        private bool DireccioneExists(long id)
        {
          return (_context.Direcciones?.Any(e => e.IdDireccion == id)).GetValueOrDefault();
        }
    }
}
