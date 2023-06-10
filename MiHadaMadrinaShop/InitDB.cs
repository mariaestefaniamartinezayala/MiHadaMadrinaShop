using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MiHadaMadrinaShop.Models;
using MiHadaMadrinaShop.Models.ViewModels;
using System;
using System.Threading.Tasks;

namespace MiHadaMadrinaShop
{
    public static class InitDB
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            // Roles
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

            // Usuario admin
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

            // Sexos
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MiHadaMadrinaHandMadeDBContext>();

                var sexos = new[] { "Mujer", "Hombre", "Otros" };

                foreach (var sexo in sexos)
                {
                    if (!await dbContext.Sexos.AnyAsync(s => s.Sexo1 == sexo))
                    {
                        await dbContext.Sexos.AddAsync(new Sexo
                        {
                            Sexo1 = sexo
                        });
                    }
                }

                await dbContext.SaveChangesAsync();
            }

            // Estados
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MiHadaMadrinaHandMadeDBContext>();

                var estados = new[] { "Recibido", "Pendiente de pago", "Procesando", "Preparando envío", "Enviado", "Entregado", "Facturado" };

                foreach (var estado in estados)
                {
                    if (!await dbContext.Estados.AnyAsync(e => e.Estado1 == estado))
                    {
                        await dbContext.Estados.AddAsync(new Estado
                        {
                            Estado1 = estado
                        });
                    }
                }

                await dbContext.SaveChangesAsync();
            }

            // Formas de pago
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MiHadaMadrinaHandMadeDBContext>();

                var formaDePago = new[] { "Transferencia", "Tarjeta", "Paypal" };

                foreach (var fp in formaDePago)
                {
                    if (!await dbContext.FormasDePagos.AnyAsync(x => x.FormaDePago == fp))
                    {
                        await dbContext.FormasDePagos.AddAsync(new FormasDePago
                        {
                            FormaDePago = fp
                        });
                    }
                }

                await dbContext.SaveChangesAsync();
            }

            // Formas de entrega
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MiHadaMadrinaHandMadeDBContext>();

                var formasDeEntrega = new[] { "Envío a domicilio", "Envío a un punto de entrega" };

                foreach (var fe in formasDeEntrega)
                {
                    if (!await dbContext.FormasDeEntregas.AnyAsync(x => x.FormaDeEntrega == fe))
                    {
                        await dbContext.FormasDeEntregas.AddAsync(new FormasDeEntrega
                        {
                            FormaDeEntrega = fe
                        });
                    }
                }

                await dbContext.SaveChangesAsync();
            }

            // Firmas de envío
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<MiHadaMadrinaHandMadeDBContext>();

                var formasDeEnvios = new[] { "Envío est&aacute;ndar", "Envío urgente" };

                foreach (var fe in formasDeEnvios)
                {
                    if (!await dbContext.FormasDeEnvios.AnyAsync(x => x.FormaDeEnvio == fe))
                    {
                        await dbContext.FormasDeEnvios.AddAsync(new FormasDeEnvio
                        {
                            FormaDeEnvio = fe
                        });
                    }
                }

                await dbContext.SaveChangesAsync();
            }

        }
    }
}
