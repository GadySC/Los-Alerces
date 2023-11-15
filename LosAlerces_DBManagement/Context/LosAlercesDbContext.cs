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
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Personal> Personal { get; set; }
        public DbSet<Cotizacion> Cotizaciones { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // Configuración de la relación entre Cliente y Contactos
            builder.Entity<Cliente>(entity =>
            {
                entity.HasKey(e => e.ID_Cliente);
                entity.Property(e => e.Nombre_Empresa).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Direccion).IsRequired().HasMaxLength(255);
                entity.HasMany(c => c.Contactos).WithOne(e => e.Cliente).HasForeignKey(e => e.ID_Cliente);
            });

            builder.Entity<Contactos>(entity =>
            {
                entity.HasKey(e => e.ID_Contactos);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Telefono).HasMaxLength(20);
            });

            // Configuración de la entidad Productos
            builder.Entity<Productos>(entity =>
            {
                entity.HasKey(e => e.ID_Productos);
                entity.Property(e => e.Nombre_Producto).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Descripcion).HasMaxLength(255);
                entity.Property(e => e.Precio).HasColumnType("DECIMAL(10, 2)");
                entity.Property(e => e.Stock).IsRequired();
            });

            // Configuración de la entidad Categoria
            builder.Entity<Categoria>(entity =>
            {
                entity.HasKey(e => e.ID_Categoria);
                entity.Property(e => e.Nombre_Categoria).IsRequired().HasMaxLength(255);
            });

            // Configuración de la entidad Personal
            builder.Entity<Personal>(entity =>
            {
                entity.HasKey(e => e.ID_Personal);
                entity.Property(e => e.Nombre).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Apellido).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Cargo).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Salario).HasColumnType("DECIMAL(10, 2)");
                entity.Property(e => e.Email).IsRequired().HasMaxLength(255);
                entity.Property(e => e.Direccion).IsRequired().HasMaxLength(255);
            });

            // Configuración de la entidad Cotizacion
            builder.Entity<Cotizacion>(entity =>
            {
                entity.HasKey(e => e.ID_Cotizacion);
                entity.HasOne(c => c.Cliente).WithMany().HasForeignKey(e => e.ID_Cliente);
                entity.HasOne(c => c.Productos).WithMany().HasForeignKey(e => e.ID_Producto);
                entity.HasOne(c => c.Personal).WithMany().HasForeignKey(e => e.ID_Personal);
                entity.Property(e => e.Fecha_cotizacion).IsRequired().HasColumnType("DATE");
                entity.Property(e => e.Cantidad).IsRequired();
                entity.Property(e => e.Precio_Unitario).HasColumnType("DECIMAL(10, 2)");
                entity.Property(e => e.Total).HasColumnType("DECIMAL(10, 2)");
            });
        }
    }
}
