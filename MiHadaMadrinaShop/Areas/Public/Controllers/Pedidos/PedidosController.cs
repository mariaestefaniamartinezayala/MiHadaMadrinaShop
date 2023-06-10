using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Primitives;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Areas.Public.Controllers.Pedidos
{
    [Area("Public")]
    public class PedidosController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _context;

        public PedidosController(MiHadaMadrinaHandMadeDBContext context)
        {
            _context = context;
        }

   

        private bool PedidoExists(long id)
        {
            return (_context.Pedidos?.Any(e => e.IdPedido == id)).GetValueOrDefault();
        }


        [HttpPost]
        public async Task<IActionResult> CrearPedido([FromBody] Pedido data)
        {
            if(data.IdFormaDeEntrega == 0 || data.IdFormaDeEnvio == 0 || data.IdFormaDePago == 0 || data.IdDireccionDomicilio == 0 || data.IdDireccionFacturacion == 0)
            {
                return Json(new { success = false, message = "Pedido no creado" });
            }
           

            var user = User.Identity.GetUserId();

            var cestaUser = _context.TCesta.Where(q => q.IdAppNetUsers.Equals(user) && q.IdPedido.Equals(null));

            

            Pedido pedido = new Pedido();
            pedido.Iva = 21;
            pedido.IdDireccionFacturacion = data.IdDireccionFacturacion;
            pedido.IdDireccionDomicilio = data.IdDireccionDomicilio;
            pedido.IdFormaDeEntrega = data.IdFormaDeEntrega;
            pedido.IdFormaDeEnvio = data.IdFormaDeEnvio;
            pedido.IdFormaDePago = data.IdFormaDePago;
            pedido.Total = data.Total;
            pedido.TotalSinIva = Math.Round((data.Total / (decimal)1.21), 2);
            pedido.FechaPedido = DateTime.Now;
            pedido.IdAspNetUsers = user;
            pedido.IdEstado = 2;

            _context.Add(pedido);
            await _context.SaveChangesAsync();


            //Ponerle a las cestas el id del pedido
            long idPedido = _context.Pedidos.Max(q => q.IdPedido);

            

            foreach (var cesta in cestaUser)
            {
                cesta.IdPedido = idPedido;
                _context.TCesta.Update(cesta);
            }
            await _context.SaveChangesAsync();

            return Json(new { success = true, message = "Pedido creado" });

        }

        [HttpGet]
        public async Task<IActionResult> PedidoRealizado()
        {

            return View(nameof(PedidoRealizado));
        }

    }
}
