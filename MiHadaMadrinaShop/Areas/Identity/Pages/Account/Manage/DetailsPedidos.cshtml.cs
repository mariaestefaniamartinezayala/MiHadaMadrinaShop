using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Identity.Pages.Account.Manage.Pedidos
{
    public class DetailsModel : PageModel
    {
        private readonly MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext _context;

        public DetailsModel(MiHadaMadrinaShop.Models.MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

        public Pedido Pedido { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(long? id)
        {
            if (id == null || _context.Pedidos == null)
            {
                return NotFound();
            }

            var pedido = await _context.Pedidos.FirstOrDefaultAsync(m => m.IdPedido == id);
            if (pedido == null)
            {
                return NotFound();
            }
            else
            {
                Pedido = pedido;
            }

            CrearFactura(pedido);

            return Page();
        }

        public void CrearFactura(Pedido pedido)
        {


            if (pedido.IdEstado == 7)
            {

                if (!_context.Facturas.Any(q => q.IdPedido.Equals(pedido.IdPedido)))
                {
                    Factura factura = new Factura();
                    factura.IdPedido = pedido.IdPedido;

                    _context.Add(factura);
                    _context.SaveChangesAsync();
                }


            }
        }
    }
}
