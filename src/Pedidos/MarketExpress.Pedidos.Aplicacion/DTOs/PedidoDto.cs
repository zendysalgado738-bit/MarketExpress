namespace MarketExpress.Pedidos.Aplicacion.DTOs;

public record PedidoDto(
    Guid Id,
    DateTime Fecha,
    string Estado,
    decimal Total,
    IReadOnlyList<DetallePedidoDto> Detalles);
    