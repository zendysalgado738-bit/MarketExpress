namespace MarketExpress.Pedidos.Aplicacion.DTOs;

public record CrearPedidoDto(
    IReadOnlyList<CrearDetallePedidoDto> Detalles);
    