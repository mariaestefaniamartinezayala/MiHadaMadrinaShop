using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MiHadaMadrinaShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
       
    }
}