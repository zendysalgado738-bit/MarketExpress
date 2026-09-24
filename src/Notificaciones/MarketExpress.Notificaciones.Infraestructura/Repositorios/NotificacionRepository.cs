using MarketExpress.Notificaciones.Aplicacion.Interfaces;
using MarketExpress.Notificaciones.Dominio.Entidades;
using MarketExpress.Notificaciones.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace MarketExpress.Notificaciones.Infraestructura.Repositorios;

public class NotificacionRepository :
    INotificacionRepository
{
    private readonly NotificacionesDbContext _contexto;

    public NotificacionRepository(
        NotificacionesDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task AgregarAsync(
        Notificacion notificacion)
    {
        await _contexto.Notificaciones
            .AddAsync(notificacion);
    }

    public async Task<List<Notificacion>>
        ObtenerTodasAsync()
    {
        return await _contexto.Notificaciones
            .AsNoTracking()
            .OrderByDescending(x => x.FechaCreacion)
            .ToListAsync();
    }

    public async Task<Notificacion?>
        ObtenerPorIdAsync(Guid id)
    {
        return await _contexto.Notificaciones
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task GuardarCambiosAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}
