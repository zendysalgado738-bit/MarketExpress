namespace MarketExpress.Pedidos.Aplicacion.DTOs;

public record DetallePedidoDto(
    Guid ProductoId,
    string NombreProducto,
    int Cantidad,
    decimal PrecioUnitario,
    decimal Subtotal);
    