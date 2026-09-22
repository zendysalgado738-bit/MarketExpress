using MarketExpress.Pedidos.Aplicacion.DTOs;

namespace MarketExpress.Pedidos.Aplicacion.Interfaces;

public interface IProductoCatalogoClient
{
    Task<ProductoCatalogoDto?> ObtenerPorIdAsync(Guid id);

    Task<ProductoCatalogoDto?> ReservarStockAsync(
        Guid id,
        int cantidad);
}
