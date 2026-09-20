using MarketExpress.Catalogo.Aplicacion.DTOs;
using MarketExpress.Catalogo.Aplicacion.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MarketExpress.Catalogo.API.Controllers;

[ApiController]
[Route("api/productos")]
public class ProductosController : ControllerBase
{
    private readonly IProductoService _productos;

    public ProductosController(IProductoService productos)
    {
        _productos = productos;
    }

    [HttpPost]
    public async Task<IActionResult> Crear([FromBody] CrearProductoDto datos)
    {
        try
        {
            var producto = await _productos.CrearAsync(datos);
            return CreatedAtAction(nameof(ObtenerPorId),
                new { id = producto.Id }, producto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var productos = await _productos.ListarAsync();
        return Ok(productos);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> ObtenerPorId(Guid id)
    {
        var producto = await _productos.ObtenerPorIdAsync(id);
        return producto is null ? NotFound() : Ok(producto);
    }

    [HttpPost("{id:guid}/reservar-stock")]
    public async Task<IActionResult> ReservarStock(
        Guid id, [FromBody] ReservarStockDto datos)
    {
        try
        {
            var producto = await _productos.ReservarStockAsync(id, datos.Cantidad);
            return producto is null ? NotFound() : Ok(producto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
    }
}