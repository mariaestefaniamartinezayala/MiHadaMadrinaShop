using System;
using System.Collections.Generic;

namespace MiHadaMadrinaShop.Models
{
    public partial class AspNetUser
    {
        public AspNetUser()
        {
            AspNetUserClaims = new HashSet<AspNetUserClaim>();
            AspNetUserLogins = new HashSet<AspNetUserLogin>();
            AspNetUserTokens = new HashSet<AspNetUserToken>();
            Direcciones = new HashSet<Direccione>();
            Pedidos = new HashSet<Pedido>();
            TCesta = new HashSet<TCestum>();
            Roles = new HashSet<AspNetRole>();
        }

        public string Id { get; set; } = null!;
        public string? UserName { get; set; }
        public string? NormalizedUserName { get; set; }
        public string? Email { get; set; }
        public string? NormalizedEmail { get; set; }
        public bool? EmailConfirmed { get; set; }
        public string? PasswordHash { get; set; }
        public string? SecurityStamp { get; set; }
        public string? ConcurrencyStamp { get; set; }
        public string? PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public bool? TwoFactorEnabled { get; set; }
        public DateTimeOffset? LockoutEnd { get; set; }
        public bool LockoutEnabled { get; set; }
        public int AccessFailedCount { get; set; }
        public string? Nombre { get; set; }
        public string? Apellidos { get; set; }
        public DateTime? FechaNacimiento { get; set; }
        public byte? IdSexo { get; set; }

        public virtual Sexo? IdSexoNavigation { get; set; }
        public virtual ICollection<AspNetUserClaim> AspNetUserClaims { get; set; }
        public virtual ICollection<AspNetUserLogin> AspNetUserLogins { get; set; }
        public virtual ICollection<AspNetUserToken> AspNetUserTokens { get; set; }
        public virtual ICollection<Direccione> Direcciones { get; set; }
        public virtual ICollection<Pedido> Pedidos { get; set; }
        public virtual ICollection<TCestum> TCesta { get; set; }

        public virtual ICollection<AspNetRole> Roles { get; set; }
    }
}
