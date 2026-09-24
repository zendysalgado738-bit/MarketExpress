using MarketExpress.Notificaciones.Aplicacion.DTOs;
using MarketExpress.Notificaciones.Aplicacion.Interfaces;
using MarketExpress.Notificaciones.Dominio.Entidades;

namespace MarketExpress.Notificaciones.Aplicacion.Servicios;

public class NotificacionService : INotificacionService
{
    private readonly INotificacionRepository _repositorio;

    public NotificacionService(INotificacionRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<NotificacionDto> RegistrarAsync(
        CrearNotificacionDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var notificacion = new Notificacion(
            dto.PedidoId,
            dto.Destinatario,
            dto.Mensaje);

        await _repositorio.AgregarAsync(notificacion);
        await _repositorio.GuardarCambiosAsync();

        return ConvertirADto(notificacion);
    }

    public async Task<List<NotificacionDto>> ListarAsync()
    {
        var notificaciones =
            await _repositorio.ObtenerTodasAsync();

        return notificaciones
            .Select(ConvertirADto)
            .ToList();
    }

    public async Task<NotificacionDto?> ObtenerPorIdAsync(
        Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException(
                "El identificador de la notificación es obligatorio.",
                nameof(id));
        }

        var notificacion =
            await _repositorio.ObtenerPorIdAsync(id);

        return notificacion is null
            ? null
            : ConvertirADto(notificacion);
    }

    private static NotificacionDto ConvertirADto(
        Notificacion notificacion)
    {
        return new NotificacionDto
        {
            Id = notificacion.Id,
            PedidoId = notificacion.PedidoId,
            Destinatario = notificacion.Destinatario,
            Mensaje = notificacion.Mensaje,
            Estado = notificacion.Estado,
            FechaCreacion = notificacion.FechaCreacion
        };
    }
}
