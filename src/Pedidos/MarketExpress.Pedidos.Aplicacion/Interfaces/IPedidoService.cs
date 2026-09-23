using MarketExpress.Pedidos.Aplicacion.DTOs;

namespace MarketExpress.Pedidos.Aplicacion.Interfaces;

public interface IPedidoService
{
    Task<PedidoDto> CrearAsync(CrearPedidoDto datos);
    Task<PedidoDto?> ObtenerPorIdAsync(Guid id);
}
