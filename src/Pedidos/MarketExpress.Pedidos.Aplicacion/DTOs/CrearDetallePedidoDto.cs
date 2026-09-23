namespace MarketExpress.Pedidos.Aplicacion.DTOs;

public record CrearDetallePedidoDto(
    Guid ProductoId,
    int Cantidad);
    