using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LosAlerces_Login.Entities;
using LosAlerces_Login.Models;

namespace LosAlerces_Login.Context
{
    public class LosAlercesDbContext : IdentityDbContext<ApplicationUser>
    {
        public LosAlercesDbContext(DbContextOptions<LosAlercesDbContext> options)
        : base(options)
        {
        }

        // DbSets para tus entidades
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contactos> Contactos { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de las tablas de Identity
            builder.Entity<IdentityUser>(entity => { entity.ToTable(name: "Usuarios"); });
            builder.Entity<IdentityRole>(entity => { entity.ToTable(name: "Roles"); });
            builder.Entity<IdentityUserRole<string>>(entity => { entity.ToTable("UserRoles"); });
            builder.Entity<IdentityUserClaim<string>>(entity => { entity.ToTable("UserClaims"); });
            builder.Entity<IdentityUserLogin<string>>(entity => { entity.ToTable("UserLogins"); });
            builder.Entity<IdentityRoleClaim<string>>(entity => { entity.ToTable("RoleClaims"); });
            builder.Entity<IdentityUserToken<string>>(entity => { entity.ToTable("UserTokens"); });

            builder.Entity<ApplicationUser>(entity =>
            {
                entity.HasIndex(u => u.Rut).IsUnique(); // Esto asegura que el Rut sea único en la base de datos
            });
        }
    }
}
