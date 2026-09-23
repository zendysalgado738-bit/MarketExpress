
using MarketExpress.Pedidos.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MarketExpress.Pedidos.Infraestructura.Persistencia;

public class PedidosDbContext : DbContext
{
    public PedidosDbContext(
        DbContextOptions<PedidosDbContext> options)
        : base(options)
    {
    }

    public DbSet<Pedido> Pedidos => Set<Pedido>();
    public DbSet<DetallePedido> DetallesPedidos => Set<DetallePedido>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Pedido>(pedido =>
        {
            pedido.ToTable("Pedidos");

            pedido.HasKey(p => p.Id);

            pedido.Property(p => p.Fecha)
                .IsRequired();

            pedido.Property(p => p.Estado)
                .IsRequired()
                .HasMaxLength(30);

            pedido.Property(p => p.Total)
                .HasPrecision(18, 2);

            pedido.HasMany(p => p.Detalles)
                .WithOne()
                .HasForeignKey(d => d.PedidoId)
                .OnDelete(DeleteBehavior.Cascade);

            pedido.Navigation(p => p.Detalles)
                .UsePropertyAccessMode(PropertyAccessMode.Field);
        });

        modelBuilder.Entity<DetallePedido>(detalle =>
        {
            detalle.ToTable("DetallesPedidos");

            detalle.HasKey(d => d.Id);

            detalle.Property(d => d.PedidoId)
                .IsRequired();

            detalle.Property(d => d.ProductoId)
                .IsRequired();

            detalle.Property(d => d.NombreProducto)
                .IsRequired()
                .HasMaxLength(200);

            detalle.Property(d => d.Cantidad)
                .IsRequired();

            detalle.Property(d => d.PrecioUnitario)
                .HasPrecision(18, 2);

            detalle.Property(d => d.Subtotal)
                .HasPrecision(18, 2);
        });
    }
}