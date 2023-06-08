using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;


namespace MiHadaMadrinaShop.Areas.Identity.Pages.Account.Manage.Direcciones
{
    public class IndexModel : PageModel
    {
        private readonly MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext _context;

        public IndexModel(MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        public IList<Direccione> Direccione { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Direcciones != null)
            {
                Direccione = await _context.Direcciones.Where(q => q.IdAspNetUsers.Equals(User.Identity.GetUserId())).ToListAsync();
            }
        }
    }
}
