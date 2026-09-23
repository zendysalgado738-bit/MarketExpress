using MarketExpress.Pedidos.Aplicacion.DTOs;
using MarketExpress.Pedidos.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketExpress.Pedidos.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PedidosController : ControllerBase
{
    private readonly IPedidoService _pedidoService;

    public PedidosController(IPedidoService pedidoService)
    {
        _pedidoService = pedidoService;
    }

    [HttpPost]
    public async Task<ActionResult<PedidoDto>> Crear(
        CrearPedidoDto datos)
    {
        try
        {
            var pedido = await _pedidoService.CrearAsync(datos);

            return CreatedAtAction(
                nameof(ObtenerPorId),
                new { id = pedido.Id },
                pedido);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (HttpRequestException)
        {
            return StatusCode(
                StatusCodes.Status503ServiceUnavailable,
                new
                {
                    error = "No se pudo comunicar con el servicio de Catálogo."
                });
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<PedidoDto>> ObtenerPorId(Guid id)
    {
        try
        {
            var pedido = await _pedidoService.ObtenerPorIdAsync(id);

            if (pedido is null)
                return NotFound(new
                {
                    error = "No se encontró el pedido solicitado."
                });

            return Ok(pedido);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}