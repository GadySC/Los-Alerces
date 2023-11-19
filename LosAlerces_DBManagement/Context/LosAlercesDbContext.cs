using LosAlerces_DBManagement.Entities;
using Microsoft.EntityFrameworkCore;

namespace LosAlerces_DBManagement.Context
{
    public class LosAlercesDbContext : DbContext
    {
        public LosAlercesDbContext(DbContextOptions<LosAlercesDbContext> options)
            : base(options)
        {
        }

        // DbSets para tus entidades
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Contactos> Contactos { get; set; }
        public DbSet<Productos> Productos { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }
        public DbSet<ProductoCotizacion> ProductosCotizacion { get; set; }
        public DbSet<PersonalCotizacion> PersonalCotizacion { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de la relación entre Cliente y Contactos
            builder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ID_Cliente);
                entity.Property(e => e.name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.address).IsRequired().HasMaxLength(255);
                entity.Property(e => e.phone).IsRequired().HasMaxLength(255);
                entity.Property(e => e.email).IsRequired().HasMaxLength(255);

                entity.HasOne(c => c.Contacto)
                      .WithOne(co => co.Cliente)
                      .HasForeignKey<Contactos>(co => co.ID_Cliente)
                      .IsRequired(false);
            });

            builder.Entity<Contactos>(entity =>
            {
                entity.HasKey(e => e.ID_Contactos);
                entity.Property(e => e.name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.lastname).IsRequired().HasMaxLength(255);
                entity.Property(e => e.email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.phone).HasMaxLength(20);
            });

            // Configuración de la entidad Productos
            builder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.ID_Productos);
                entity.Property(e => e.name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.note).HasMaxLength(255);
                entity.Property(e => e.price).HasColumnType("DECIMAL(10, 2)");
            });

            // Configuración de la entidad Personal
            builder.Entity<Personal>(entity =>
            {
                entity.HasKey(e => e.ID_Personal);
                entity.Property(e => e.name).IsRequired().HasMaxLength(255);
                entity.Property(e => e.lastname).IsRequired().HasMaxLength(255);
                entity.Property(e => e.profession).IsRequired().HasMaxLength(255);
                entity.Property(e => e.salary).HasColumnType("DECIMAL(10, 2)");
                entity.Property(e => e.email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.address).IsRequired().HasMaxLength(255);
                entity.Property(e => e.phone).IsRequired().HasMaxLength(255);
            });

            // Configuración de la entidad Cotizacion
            builder.Entity<Cotizacion>(entity =>
            {
                entity.HasKey(e => e.ID_Cotizacion);
                entity.HasOne(c => c.Cliente).WithMany().HasForeignKey(c => c.ID_Cliente);
                entity.Property(e => e.QuotationDate).IsRequired().HasColumnType("DATE");
                entity.Property(e => e.quantityofproduct).IsRequired();
                entity.HasMany(c => c.ProductosCotizacion)
                      .WithOne(p => p.Cotizacion)
                      .HasForeignKey(p => p.ID_Cotizacion);
                entity.HasMany(c => c.PersonalCotizacion)
                      .WithOne(p => p.Cotizacion)
                      .HasForeignKey(p => p.ID_Cotizacion);
            });

            // Configuraciones para las relaciones de muchos a muchos
            builder.Entity<ProductoCotizacion>()
                .HasKey(pc => new { pc.ID_Cotizacion, pc.ID_Producto });

            builder.Entity<PersonalCotizacion>()
                .HasKey(pc => new { pc.ID_Cotizacion, pc.ID_Personal });

        }
    }
}
