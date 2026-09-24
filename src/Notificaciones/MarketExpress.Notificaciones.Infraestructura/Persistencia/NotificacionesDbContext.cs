using MarketExpress.Notificaciones.Dominio.Entidades;
using Microsoft.EntityFrameworkCore;

namespace MarketExpress.Notificaciones.Infraestructura.Persistencia;

public class NotificacionesDbContext : DbContext
{
    public NotificacionesDbContext(
        DbContextOptions<NotificacionesDbContext> options)
        : base(options)
    {
    }

    public DbSet<Notificacion> Notificaciones =>
        Set<Notificacion>();

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var notificacion =
            modelBuilder.Entity<Notificacion>();

        notificacion.ToTable("Notificaciones");

        notificacion.HasKey(x => x.Id);

        notificacion.Property(x => x.PedidoId)
            .IsRequired();

        notificacion.Property(x => x.Destinatario)
            .IsRequired()
            .HasMaxLength(200);

        notificacion.Property(x => x.Mensaje)
            .IsRequired()
            .HasMaxLength(500);

        notificacion.Property(x => x.Estado)
            .IsRequired()
            .HasMaxLength(30);

        notificacion.Property(x => x.FechaCreacion)
            .IsRequired();

        notificacion.HasIndex(x => x.PedidoId);
    }
}
