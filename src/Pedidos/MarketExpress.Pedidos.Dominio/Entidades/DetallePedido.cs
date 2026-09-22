namespace MarketExpress.Pedidos.Dominio.Entidades;

public class DetallePedido
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public Guid ProductoId { get; private set; }
    public string NombreProducto { get; private set; }
    public int Cantidad { get; private set; }
    public decimal PrecioUnitario { get; private set; }
    public decimal Subtotal { get; private set; }

    private DetallePedido()
    {
        NombreProducto = string.Empty;
    }

    public DetallePedido(
        Guid productoId,
        string nombreProducto,
        int cantidad,
        decimal precioUnitario)
    {
        if (productoId == Guid.Empty)
            throw new ArgumentException(
                "El identificador del producto es obligatorio.",
                nameof(productoId));

        if (string.IsNullOrWhiteSpace(nombreProducto))
            throw new ArgumentException(
                "El nombre del producto es obligatorio.",
                nameof(nombreProducto));

        if (cantidad <= 0)
            throw new ArgumentException(
                "La cantidad debe ser mayor que cero.",
                nameof(cantidad));

        if (precioUnitario <= 0)
            throw new ArgumentException(
                "El precio unitario debe ser mayor que cero.",
                nameof(precioUnitario));

        Id = Guid.NewGuid();
        ProductoId = productoId;
        NombreProducto = nombreProducto.Trim();
        Cantidad = cantidad;
        PrecioUnitario = precioUnitario;
        Subtotal = cantidad * precioUnitario;
    }
}
