using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
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
    }
}
