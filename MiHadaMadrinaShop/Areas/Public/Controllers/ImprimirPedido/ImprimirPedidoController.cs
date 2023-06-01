using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;
using Rotativa.AspNetCore;

namespace MiHadaMadrinaShop.Areas.Public.Controllers.ImprimirPedido
{
    public class ImprimirPedidoController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _dbcontex;

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImprimirPedido(int idPedido)
        {
            // Obtenemos la cabecera del pedido
            Pedido modelo = (Pedido)_dbcontex.Pedidos.Include(dp => dp.IdPedido).Where(c => c.IdPedido == idPedido);
             



            //Retornamos el PDF
            return new ViewAsPdf("ImprimirPedido", modelo)
            {
                FileName = $"Pedido {modelo.IdPedido}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}
