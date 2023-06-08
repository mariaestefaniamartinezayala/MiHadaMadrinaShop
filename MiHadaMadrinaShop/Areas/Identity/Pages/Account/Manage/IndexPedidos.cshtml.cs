using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Identity.Pages.Account.Manage.Pedidos
{
    public class IndexModel : PageModel
    {
        private readonly MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext _context;

        public IndexModel(MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        public IList<Pedido> Pedido { get;set; } = default!;

        public async Task OnGetAsync()
        {
            var user = User.Identity.GetUserId();

            if (_context.Pedidos != null)
            {
                Pedido = await _context.Pedidos.Where(q=>q.IdAspNetUsers.Equals(user))
                .Include(p => p.IdAspNetUsersNavigation)
                .Include(p => p.IdEstadoNavigation)
                .Include(p => p.IdFormaDeEntregaNavigation)
                .Include(p => p.IdFormaDeEnvioNavigation)
                .Include(p => p.IdFormaDePagoNavigation).ToListAsync();
            }
        }
    }
}
