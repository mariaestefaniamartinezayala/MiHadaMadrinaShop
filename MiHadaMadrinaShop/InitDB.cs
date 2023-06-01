using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MiHadaMadrinaShop.Models;
using System;
using System.Threading.Tasks;

namespace MiHadaMadrinaShop
{
    public static class InitDB
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                var roles = new[] { "Admin", "Editor", "Public" };

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role))
                    {
                        await roleManager.CreateAsync(new IdentityRole(role));
                    }
                }
            }

            using (var scope = serviceProvider.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();

                string correo = "admin@mihadamadrina.com";
                string clave = "Test-1234";

                var adminUser = await userManager.FindByEmailAsync(correo);
                if (adminUser == null)
                {
                    var user = new IdentityUser
                    {
                        UserName = correo,
                        Email = correo,
                        EmailConfirmed = true
                    };

                    await userManager.CreateAsync(user, clave);
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }

        }

        public static void CreateInitialStates(MiHadaMadrinaHandMadeDBContext dbContext)
        {
            if (!dbContext.Estados.Any())
            {
                List<string> listEstados = new()
                {
                    "Recibido",
                    "Pendiente de pago",
                    "Procesando",
                    "Preparando envío",
                    "Enviado",
                    "Entregado",
                    "Facturado"
                };

                foreach (var e in listEstados)
                {
                    var estado = new Estado { Estado1 = e };
                    dbContext.Estados.Add(estado);
                }

                dbContext.SaveChanges();
            }
        }

        public static void CreateInitialPaymentMethods(MiHadaMadrinaHandMadeDBContext dbContext)
        {
            if (!dbContext.FormasDePagos.Any())
            {
                List<string> listFormasPago = new()
                {
                    "Transferencia",
                    "Tarjeta",
                    "Paypal"
                };

                foreach (var fp in listFormasPago)
                {
                    var formaPago = new FormasDePago { FormaDePago = fp };
                    dbContext.FormasDePagos.Add(formaPago);
                }

                dbContext.SaveChanges();
            }

        }

        public static void CreateInitialShippingMethods(MiHadaMadrinaHandMadeDBContext dbContext)
        {
            if (!dbContext.FormasDeEntregas.Any())
            {
                List<string> listFormasEntrega = new()
                {
                    "Envío a domicilio",
                    "Envío a un punto de entrega"
                };

                foreach (var fe in listFormasEntrega)
                {
                    var formaEntrega = new FormasDeEntrega { FormaDeEntrega = fe };
                    dbContext.FormasDeEntregas.Add(formaEntrega);
                }

                dbContext.SaveChanges();
            }

        }

        public static void CreateInitialDeliveryMethods(MiHadaMadrinaHandMadeDBContext dbContext)
        {
            if (!dbContext.FormasDeEnvios.Any())
            {
                List<string> listFormasEnvio = new()
                {
                    "Envío urgente",
                    "Envío estándar"
                };

                foreach (var fe in listFormasEnvio)
                {
                    var formaEnvio = new FormasDeEnvio { FormaDeEnvio = fe };
                    dbContext.FormasDeEnvios.Add(formaEnvio);
                }

                dbContext.SaveChanges();
            }

        }

    }
}
