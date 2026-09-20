using MarketExpress.Catalogo.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MarketExpress.Catalogo.Infraestructura.Persistencia;

public class CatalogoDbContext : DbContext
{
    public CatalogoDbContext(DbContextOptions<CatalogoDbContext> options)
        : base(options)
    {
    }

    public DbSet<Producto> Productos => Set<Producto>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(producto =>
        {
            producto.HasKey(p => p.Id);
            producto.Property(p => p.Nombre).IsRequired();
            producto.Property(p => p.Precio).HasPrecision(18, 2);
        });
    }
}