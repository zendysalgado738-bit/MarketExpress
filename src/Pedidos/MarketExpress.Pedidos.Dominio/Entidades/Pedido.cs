namespace MarketExpress.Pedidos.Dominio.Entidades;

public class Pedido
{
    private readonly List<DetallePedido> _detalles = new();

    public Guid Id { get; private set; }
    public DateTime Fecha { get; private set; }
    public string Estado { get; private set; }
    public decimal Total { get; private set; }
    public IReadOnlyCollection<DetallePedido> Detalles => _detalles.AsReadOnly();

    private Pedido()
    {
        Estado = string.Empty;
    }

    public Pedido(IEnumerable<DetallePedido> detalles)
    {
        if (detalles is null)
            throw new ArgumentNullException(nameof(detalles));

        var listaDetalles = detalles.ToList();

        if (listaDetalles.Count == 0)
            throw new ArgumentException(
                "El pedido debe contener al menos un detalle.",
                nameof(detalles));

        Id = Guid.NewGuid();
        Fecha = DateTime.UtcNow;
        Estado = "Pendiente";

        _detalles.AddRange(listaDetalles);
        Total = _detalles.Sum(detalle => detalle.Subtotal);
    }
}
