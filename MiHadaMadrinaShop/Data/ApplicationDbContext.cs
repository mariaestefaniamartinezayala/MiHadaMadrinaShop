using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MiHadaMadrinaShop.Models;

namespace MiHadaMadrinaShop.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
       
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        //public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{

        //}

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.UseCollation("Latin1_General_CI_AS");

        //    modelBuilder.Entity<AspNetUser>(entity =>
        //    {
        //        entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

        //        entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
        //            .IsUnique()
        //            .HasFilter("([NormalizedUserName] IS NOT NULL)");

        //        entity.Property(e => e.Apellidos).HasMaxLength(100);

        //        entity.Property(e => e.Email).HasMaxLength(256);

        //        entity.Property(e => e.FechaNacimiento).HasColumnType("date");

        //        entity.Property(e => e.IdSexo).HasDefaultValueSql("((3))");

        //        entity.Property(e => e.Nombre).HasMaxLength(50);

        //        entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

        //        entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

        //        entity.Property(e => e.UserName).HasMaxLength(256);

        //        entity.HasOne(d => d.IdSexoNavigation)
        //            .WithMany(p => p.AspNetUsers)
        //            .HasForeignKey(d => d.IdSexo)
        //            .HasConstraintName("FK_AspNetUsers_Sexos");

        //        entity.HasMany(d => d.Roles)
        //            .WithMany(p => p.Users)
        //            .UsingEntity<Dictionary<string, object>>(
        //                "AspNetUserRole",
        //                l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
        //                r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
        //                j =>
        //                {
        //                    j.HasKey("UserId", "RoleId");

        //                    j.ToTable("AspNetUserRoles");

        //                    j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
        //                });
        //    });
        //    OnModelCreatingPartial(modelBuilder);
        //}
        //partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}