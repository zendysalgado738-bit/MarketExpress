using MarketExpress.Pedidos.Dominio.Entidades;

namespace MarketExpress.Pedidos.Aplicacion.Interfaces;

public interface IPedidoRepository
{
    Task AgregarAsync(Pedido pedido);
    Task<Pedido?> ObtenerPorIdAsync(Guid id);
    Task GuardarCambiosAsync();
}
