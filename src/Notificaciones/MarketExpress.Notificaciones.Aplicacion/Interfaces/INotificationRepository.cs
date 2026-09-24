using MarketExpress.Notificaciones.Dominio.Entidades;

namespace MarketExpress.Notificaciones.Aplicacion.Interfaces;

public interface INotificacionRepository
{
    Task AgregarAsync(Notificacion notificacion);
    Task<List<Notificacion>> ObtenerTodasAsync();
    Task<Notificacion?> ObtenerPorIdAsync(Guid id);
    Task GuardarCambiosAsync();
}
