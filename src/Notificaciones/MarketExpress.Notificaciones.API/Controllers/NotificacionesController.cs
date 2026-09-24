using MarketExpress.Notificaciones.Aplicacion.DTOs;
using MarketExpress.Notificaciones.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketExpress.Notificaciones.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class NotificacionesController : ControllerBase
{
    private readonly INotificacionService _servicio;

    public NotificacionesController(
        INotificacionService servicio)
    {
        _servicio = servicio;
    }

    [HttpPost]
    public async Task<ActionResult<NotificacionDto>> Registrar(
        CrearNotificacionDto dto)
    {
        try
        {
            var notificacion =
                await _servicio.RegistrarAsync(dto);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = notificacion.Id },
                notificacion);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificacionDto>>> Listar()
    {
        var notificaciones =
            await _servicio.ListarAsync();

        return Ok(notificaciones);
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<NotificacionDto>> ObtenerPorId(
        Guid id)
    {
        try
        {
            var notificacion =
                await _servicio.ObtenerPorIdAsync(id);

            if (notificacion is null)
            {
                return NotFound(new
                {
                    error = "La notificación no existe."
                });
            }

            return Ok(notificacion);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}