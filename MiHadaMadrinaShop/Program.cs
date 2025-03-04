using MiHadaMadrinaShop;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Data;
using MiHadaMadrinaShop.Models;
using System.Diagnostics;
using System.Globalization;
using MiHadaMadrinaShop.Models.ViewModels;
using Microsoft.AspNetCore.Localization;

public class Program
{
    private static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)//Al ponerlo a false no requiere confirmar la cuenta, en producci&oacute;n debe estar a true
            .AddRoles<IdentityRole>()//Para que funcionen los roles
            .AddEntityFrameworkStores<ApplicationDbContext>();

        //builder.Services.AddDefaultIdentity<IdentityUser>().AddDefaultTokenProviders().AddRoles<IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<MiHadaMadrinaHandMadeDBContext>(options => options.UseSqlServer(connectionString));

        builder.Services.AddRazorPages();

        //builder.Services.AddControllersWithViews(options =>
        //{
        //    // Registrar el filtro de autorizaci&oacute;n personalizado
        //    options.Filters.Add(typeof(AdminAuthorizationFilter));
        //});


        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "MyArea",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "Model",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{model?}");

        app.MapAreaControllerRoute(
            name: "admin_area",
            areaName: "Admin",
            pattern: "/Admin/{controller=Pedidos}/{action=Index}/{id?}");

        app.MapRazorPages();


        // Configuraci&oacute;n de la librer�a Rotativa. Es la encargada de generar los PDFs
        IWebHostEnvironment env = app.Environment;
        Rotativa.AspNetCore.RotativaConfiguration.Setup(env.WebRootPath, "../Rotativa/Windows");


        //LLamamos a la clase InitDB para crear los roles y el usuario admin en la base de datos.
        using (var scope = app.Services.CreateScope())
        {
            var serviceProvider = scope.ServiceProvider;
            await InitDB.InitializeAsync(serviceProvider);

            // Obtenemos una instancia del contexto de base de datos
            var dbContext = serviceProvider.GetRequiredService<MiHadaMadrinaHandMadeDBContext>();
        }

        var defaultCulture = new CultureInfo("es-UY");
        var localizationOptions = new RequestLocalizationOptions
        {
            DefaultRequestCulture = new RequestCulture(defaultCulture),
            SupportedCultures = new List<CultureInfo> { defaultCulture },
            SupportedUICultures = new List<CultureInfo> { defaultCulture }
        };
        app.UseRequestLocalization(localizationOptions);

        // Agregar filtro de autorizaci&oacute;n
        //app.UseAuthorizationWithAdminFilter();
       

        app.Run();
    }
}