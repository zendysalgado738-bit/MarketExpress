using MarketExpress.Catalogo.Aplicacion.DTOs;

namespace MarketExpress.Catalogo.Aplicacion.Interfaces;

public interface IProductoService
{
    Task<ProductoDto> CrearAsync(CrearProductoDto datos);
    Task<IReadOnlyList<ProductoDto>> ListarAsync();
    Task<ProductoDto?> ObtenerPorIdAsync(Guid id);
    Task<ProductoDto?> ReservarStockAsync(Guid id, int cantidad);
}