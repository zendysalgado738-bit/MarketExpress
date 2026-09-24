using MarketExpress.Notificaciones.Aplicacion.DTOs;

namespace MarketExpress.Notificaciones.Aplicacion.Interfaces;

public interface INotificacionService
{
    Task<NotificacionDto> RegistrarAsync(CrearNotificacionDto dto);

    Task<List<NotificacionDto>> ListarAsync();

    Task<NotificacionDto?> ObtenerPorIdAsync(Guid id);
}
