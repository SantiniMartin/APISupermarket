namespace ComparadorPreciosAPI.Data
{
using Microsoft.EntityFrameworkCore;
using ComparadorPreciosAPI.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Supermercado> Supermercados { get; set; }
    public DbSet<Marca> Marcas { get; set; }
    public DbSet<Producto> Productos { get; set; }
    public DbSet<ProductoSupermercado> ProductoSupermercados { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<SupermercadoMarca> SupermercadoMarcas { get; set; }
    public DbSet<Carrito> Carritos { get; set; }
    public DbSet<CarritoItem> CarritoItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<SupermercadoMarca>()
            .HasKey(sm => new { sm.SupermercadoId, sm.MarcaId });

        modelBuilder.Entity<SupermercadoMarca>()
            .HasOne(sm => sm.Supermercado)
            .WithMany(s => s.Marcas)
            .HasForeignKey(sm => sm.SupermercadoId);

        modelBuilder.Entity<SupermercadoMarca>()
            .HasOne(sm => sm.Marca)
            .WithMany(m => m.SupermercadoMarcas)
            .HasForeignKey(sm => sm.MarcaId);

        modelBuilder.Entity<CarritoItem>()
            .HasOne(ci => ci.ProductoSupermercado)
            .WithMany()
            .HasForeignKey(ci => ci.ProductoSupermercadoId);

        modelBuilder.Entity<Carrito>()
            .HasMany(c => c.Items)
            .WithOne()
            .HasForeignKey(ci => ci.CarritoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
}
