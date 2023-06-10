using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;
using Rotativa.AspNetCore;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using System.ComponentModel.DataAnnotations.Schema;

namespace MiHadaMadrinaShop.Areas.Public.Controllers.ImprimirPDF
{
    [Area("Public")]
    public class ImprimirPDFController : Controller
    {
        private readonly MiHadaMadrinaHandMadeDBContext _dbContext;

        public ImprimirPDFController(MiHadaMadrinaHandMadeDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ImprimirPDF(int id)
        {
            // Obtenemos la cabecera del pedido
            Pedido modelo = _dbContext.Pedidos.Include(c => c.TCesta).Where(p => p.IdPedido == id)
                .Select(p => new Pedido()
                {
                    IdPedido = p.IdPedido,
                    IdDireccionDomicilio = p.IdDireccionDomicilio,
                    IdDireccionFacturacion = p.IdDireccionFacturacion,
                    IdEstado = p.IdEstado,
                    IdAspNetUsers = p.IdAspNetUsers,
                    IdFormaDeEntrega = p.IdFormaDeEntrega,
                    IdFormaDeEnvio = p.IdFormaDeEnvio,
                    IdFormaDePago = p.IdFormaDePago,
                    Iva = p.Iva,
                    PorcentajeDescuento = p.PorcentajeDescuento,
                    Total = p.Total,
                    TotalSinIva = p.TotalSinIva,
                    FechaPedido = p.FechaPedido,
                    FechaEnvio = p.FechaEnvio,
                    TCesta = p.TCesta.Select(c => new TCestum()
                    {
                        IdCesta = c.IdCesta,//borrar
                        Cantidad = c.Cantidad,
                        IdProducto = c.IdProducto,
                        Iva = c.Iva,
                        PorcentajeDeDescuento = c.PorcentajeDeDescuento,
                        Total = c.Total,
                        TotalSinIva = c.TotalSinIva,
                        IdAppNetUsers = c.IdAppNetUsers,
                        IdDatosUsuario = c.IdDatosUsuario,
                        IdPedido = c.IdPedido,
                        TotalCesta = c.TotalCesta,
                        NombreProducto = c.NombreProducto,
                        PrecioProducto = c.PrecioProducto,
                        DescuentoProducto = c.DescuentoProducto
                    }).ToList()
                }).FirstOrDefault();

            //Retornamos el PDF, para ello usamos un método del paquete Rotativa.
            return new ViewAsPdf("ImprimirPDF", modelo)
            {
                FileName = $"Pedido {modelo.IdPedido}.pdf",
                PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait,
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };
        }
    }
}

