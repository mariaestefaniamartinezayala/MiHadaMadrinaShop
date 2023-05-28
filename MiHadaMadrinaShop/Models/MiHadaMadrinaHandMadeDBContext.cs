using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace MiHadaMadrinaShop.Models
{
    public partial class MiHadaMadrinaHandMadeDBContext : DbContext
    {
        public MiHadaMadrinaHandMadeDBContext()
        {
        }

        public MiHadaMadrinaHandMadeDBContext(DbContextOptions<MiHadaMadrinaHandMadeDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AspNetRole> AspNetRoles { get; set; } = null!;
        public virtual DbSet<AspNetRoleClaim> AspNetRoleClaims { get; set; } = null!;
        public virtual DbSet<AspNetUser> AspNetUsers { get; set; } = null!;
        public virtual DbSet<AspNetUserClaim> AspNetUserClaims { get; set; } = null!;
        public virtual DbSet<AspNetUserLogin> AspNetUserLogins { get; set; } = null!;
        public virtual DbSet<AspNetUserToken> AspNetUserTokens { get; set; } = null!;
        public virtual DbSet<Categoria> Categorias { get; set; } = null!;
        public virtual DbSet<Comentario> Comentarios { get; set; } = null!;
        public virtual DbSet<Direccione> Direcciones { get; set; } = null!;
        public virtual DbSet<Estado> Estados { get; set; } = null!;
        public virtual DbSet<Factura> Facturas { get; set; } = null!;
        public virtual DbSet<FormasDeEntrega> FormasDeEntregas { get; set; } = null!;
        public virtual DbSet<FormasDeEnvio> FormasDeEnvios { get; set; } = null!;
        public virtual DbSet<FormasDePago> FormasDePagos { get; set; } = null!;
        public virtual DbSet<Pedido> Pedidos { get; set; } = null!;
        public virtual DbSet<Producto> Productos { get; set; } = null!;
        public virtual DbSet<ProductosPedido> ProductosPedidos { get; set; } = null!;
        public virtual DbSet<Sexo> Sexos { get; set; } = null!;
        public virtual DbSet<Subcategoria> Subcategorias { get; set; } = null!;
        public virtual DbSet<TCestum> TCesta { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=localhost; Database=MiHadaMadrinaHandMadeDB; Integrated Security=True; Encrypt=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("Latin1_General_CI_AS");

            modelBuilder.Entity<AspNetRole>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName, "RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetRoleClaim>(entity =>
            {
                entity.HasIndex(e => e.RoleId, "IX_AspNetRoleClaims_RoleId");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetUser>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail, "EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName, "UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Apellidos).HasMaxLength(100);

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.FechaNacimiento).HasColumnType("date");

                entity.Property(e => e.Nombre).HasMaxLength(50);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                entity.HasOne(d => d.IdSexoNavigation)
                    .WithMany(p => p.AspNetUsers)
                    .HasForeignKey(d => d.IdSexo)
                    .HasConstraintName("FK_AspNetUsers_Sexos");

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "AspNetUserRole",
                        l => l.HasOne<AspNetRole>().WithMany().HasForeignKey("RoleId"),
                        r => r.HasOne<AspNetUser>().WithMany().HasForeignKey("UserId"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId");

                            j.ToTable("AspNetUserRoles");

                            j.HasIndex(new[] { "RoleId" }, "IX_AspNetUserRoles_RoleId");
                        });
            });

            modelBuilder.Entity<AspNetUserClaim>(entity =>
            {
                entity.HasIndex(e => e.UserId, "IX_AspNetUserClaims_UserId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId, "IX_AspNetUserLogins_UserId");

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.IdCategoria);

                entity.Property(e => e.Categoria1)
                    .HasMaxLength(100)
                    .HasColumnName("Categoria");

                entity.Property(e => e.Descripcion).HasMaxLength(300);
            });

            modelBuilder.Entity<Comentario>(entity =>
            {
                entity.HasKey(e => e.IdComentario);

                entity.HasIndex(e => e.IdProductoPedido, "IX_Comentarios_IdProductoPedido");

                entity.Property(e => e.Comentario1)
                    .HasColumnType("ntext")
                    .HasColumnName("Comentario");

                entity.HasOne(d => d.IdProductoPedidoNavigation)
                    .WithMany(p => p.Comentarios)
                    .HasForeignKey(d => d.IdProductoPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comentarios_Productos_Pedido");
            });

            modelBuilder.Entity<Direccione>(entity =>
            {
                entity.HasKey(e => e.IdDireccion);

                entity.Property(e => e.CodPostal)
                    .HasMaxLength(5)
                    .IsFixedLength();

                entity.Property(e => e.Direccion).HasMaxLength(250);

                entity.Property(e => e.IdAspNetUsers).HasMaxLength(450);

                entity.Property(e => e.Localidad).HasMaxLength(40);

                entity.Property(e => e.Pais).HasMaxLength(40);

                entity.Property(e => e.Provincia).HasMaxLength(50);

                entity.HasOne(d => d.IdAspNetUsersNavigation)
                    .WithMany(p => p.Direcciones)
                    .HasForeignKey(d => d.IdAspNetUsers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Direcciones_AspNetUsers");
            });

            modelBuilder.Entity<Estado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.Property(e => e.Estado1)
                    .HasMaxLength(50)
                    .HasColumnName("Estado");
            });

            modelBuilder.Entity<Factura>(entity =>
            {
                entity.HasKey(e => e.IdFactura);

                entity.HasIndex(e => e.IdPedido, "IX_Facturas_IdPedido");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.Facturas)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Facturas_Pedidos");
            });

            modelBuilder.Entity<FormasDeEntrega>(entity =>
            {
                entity.HasKey(e => e.IdFormaDeEntrega);

                entity.ToTable("FormasDeEntrega");

                entity.Property(e => e.IdFormaDeEntrega).ValueGeneratedOnAdd();

                entity.Property(e => e.FormaDeEntrega).HasMaxLength(10);
            });

            modelBuilder.Entity<FormasDeEnvio>(entity =>
            {
                entity.HasKey(e => e.IdFormaDeEnvio)
                    .HasName("PK_FormaDeEnvio");

                entity.ToTable("FormasDeEnvio");

                entity.Property(e => e.IdFormaDeEnvio).ValueGeneratedOnAdd();

                entity.Property(e => e.FormaDeEnvio).HasMaxLength(50);
            });

            modelBuilder.Entity<FormasDePago>(entity =>
            {
                entity.HasKey(e => e.IdFormaDePago)
                    .HasName("PK_FormaDePago");

                entity.ToTable("FormasDePago");

                entity.Property(e => e.IdFormaDePago).ValueGeneratedOnAdd();

                entity.Property(e => e.FormaDePago).HasMaxLength(50);
            });

            modelBuilder.Entity<Pedido>(entity =>
            {
                entity.HasKey(e => e.IdPedido);

                entity.HasIndex(e => e.IdDireccion, "IX_Pedidos_IdDireccion");

                entity.HasIndex(e => e.IdEstado, "IX_Pedidos_IdEstado");

                entity.HasIndex(e => e.IdFormaDeEntrega, "IX_Pedidos_IdFormaDeEntrega");

                entity.HasIndex(e => e.IdFormaDeEnvio, "IX_Pedidos_IdFormaDeEnvio");

                entity.HasIndex(e => e.IdFormaDePago, "IX_Pedidos_IdFormaDePago");

                entity.Property(e => e.IdAspNetUsers).HasMaxLength(450);

                entity.Property(e => e.Iva).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TotalSinIva).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdAspNetUsersNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdAspNetUsers)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_AspNetUsers");

                entity.HasOne(d => d.IdDireccionNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdDireccion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_Direcciones");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_Estados");

                entity.HasOne(d => d.IdFormaDeEntregaNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdFormaDeEntrega)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_FormasDeEntrega");

                entity.HasOne(d => d.IdFormaDeEnvioNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdFormaDeEnvio)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_FormasDeEnvio");

                entity.HasOne(d => d.IdFormaDePagoNavigation)
                    .WithMany(p => p.Pedidos)
                    .HasForeignKey(d => d.IdFormaDePago)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Pedidos_FormasDePago");
            });

            modelBuilder.Entity<Producto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.Property(e => e.DescripcionCorta).HasMaxLength(200);

                entity.Property(e => e.DescripcionLarga).HasColumnType("ntext");

                entity.Property(e => e.FechaDeEntrada).HasColumnType("datetime");

                entity.Property(e => e.Nombre).HasMaxLength(100);

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PrecioConDescuento).HasColumnType("decimal(10, 2)");

                entity.HasMany(d => d.IdCategoria)
                    .WithMany(p => p.IdProductos)
                    .UsingEntity<Dictionary<string, object>>(
                        "ProductosCategoria",
                        l => l.HasOne<Categoria>().WithMany().HasForeignKey("IdCategoria").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Productos_Categorias_Categorias"),
                        r => r.HasOne<Producto>().WithMany().HasForeignKey("IdProducto").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK_Productos_Categorias_Productos"),
                        j =>
                        {
                            j.HasKey("IdProducto", "IdCategoria");

                            j.ToTable("Productos_Categorias");

                            j.HasIndex(new[] { "IdCategoria" }, "IX_Productos_Categorias_IdCategoria");
                        });
            });

            modelBuilder.Entity<ProductosPedido>(entity =>
            {
                entity.HasKey(e => e.IdProductoPedido)
                    .HasName("PK_Producto_Pedido");

                entity.ToTable("Productos_Pedido");

                entity.HasIndex(e => e.IdPedido, "IX_Productos_Pedido_IdPedido");

                entity.HasIndex(e => e.IdProducto, "IX_Productos_Pedido_IdProducto");

                entity.Property(e => e.Precio).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdPedidoNavigation)
                    .WithMany(p => p.ProductosPedidos)
                    .HasForeignKey(d => d.IdPedido)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productos_Pedido_Pedidos");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.ProductosPedidos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Productos_Pedido_Productos");
            });

            modelBuilder.Entity<Sexo>(entity =>
            {
                entity.HasKey(e => e.IdSexo);

                entity.Property(e => e.Sexo1)
                    .HasMaxLength(15)
                    .HasColumnName("Sexo");
            });

            modelBuilder.Entity<Subcategoria>(entity =>
            {
                entity.HasKey(e => e.IdSubcategoria);

                entity.HasIndex(e => e.IdCategoria, "IX_Subcategorias_IdCategoria");

                entity.Property(e => e.Descripcion).HasMaxLength(300);

                entity.Property(e => e.Subcategoria1)
                    .HasMaxLength(100)
                    .HasColumnName("Subcategoria");

                entity.HasOne(d => d.IdCategoriaNavigation)
                    .WithMany(p => p.Subcategoria)
                    .HasForeignKey(d => d.IdCategoria)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Subcategorias_Categorias");
            });

            modelBuilder.Entity<TCestum>(entity =>
            {
                entity.HasKey(e => e.IdCesta);

                entity.ToTable("T_Cesta");

                entity.Property(e => e.IdAppNetUsers).HasMaxLength(450);

                entity.Property(e => e.Iva).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.Total).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TotalSinIva).HasColumnType("decimal(10, 2)");

                entity.HasOne(d => d.IdAppNetUsersNavigation)
                    .WithMany(p => p.TCesta)
                    .HasForeignKey(d => d.IdAppNetUsers)
                    .HasConstraintName("FK_T_Cesta_AspNetUsers");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TCesta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_T_Cesta_Productos");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
