using Microsoft.EntityFrameworkCore;
using SubastaWeb.Models.Producto;
using SubastaWeb.Models.Usuario;

public class SubastaWebContext : DbContext
{
    public SubastaWebContext(DbContextOptions<SubastaWebContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>()
            .HasKey(p => p.IdProducto); // Define the primary key

        modelBuilder.Entity<Producto>()
            .HasDiscriminator<string>("ProductoTipo")
            .HasValue<ProductoAlimento>("Alimento")
            .HasValue<ProductoRopa>("Ropa")
            .HasValue<ProductoElectronica>("Electronica")
            .HasValue<ProductoModelDBO>("ProductoModelDBO");

        // Configure decimal properties with precision and scale
        modelBuilder.Entity<Producto>()
            .Property(p => p.PrecioInicial)
            .HasColumnType("decimal(10, 2)"); // Change 18 and 2 as needed

        modelBuilder.Entity<Producto>()
            .Property(p => p.PrecioFinal)
            .HasColumnType("decimal(10, 2)"); // Change 18 and 2 as needed
    }

public DbSet<SubastaWeb.Models.Usuario.UsuarioModelDBO> UsuarioModelDBO { get; set; } = default!;

}