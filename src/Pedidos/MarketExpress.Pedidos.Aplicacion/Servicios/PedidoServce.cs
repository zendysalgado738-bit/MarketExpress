using MarketExpress.Pedidos.Aplicacion.DTOs;
using MarketExpress.Pedidos.Aplicacion.Interfaces;
using MarketExpress.Pedidos.Dominio.Entidades;

namespace MarketExpress.Pedidos.Aplicacion.Servicios;

public class PedidoService : IPedidoService
{
    private readonly IPedidoRepository _repositorio;
    private readonly IProductoCatalogoClient _catalogoClient;

    public PedidoService(
        IPedidoRepository repositorio,
        IProductoCatalogoClient catalogoClient)
    {
        _repositorio = repositorio;
        _catalogoClient = catalogoClient;
    }

    public async Task<PedidoDto> CrearAsync(CrearPedidoDto datos)
    {
        if (datos is null)
            throw new ArgumentNullException(nameof(datos));

        if (datos.Detalles is null || datos.Detalles.Count == 0)
            throw new ArgumentException(
                "El pedido debe contener al menos un producto.",
                nameof(datos));

        foreach (var detalleSolicitado in datos.Detalles)
        {
            if (detalleSolicitado.ProductoId == Guid.Empty)
                throw new ArgumentException(
                    "El identificador del producto es obligatorio.");

            if (detalleSolicitado.Cantidad <= 0)
                throw new ArgumentException(
                    "La cantidad de cada producto debe ser mayor que cero.");
        }

        var detalles = new List<DetallePedido>();

        var productosAgrupados = datos.Detalles
            .GroupBy(detalle => detalle.ProductoId);

        foreach (var grupo in productosAgrupados)
        {
            var cantidadTotal = grupo.Sum(detalle => detalle.Cantidad);

            var producto = await _catalogoClient.ObtenerPorIdAsync(grupo.Key);

            if (producto is null)
                throw new KeyNotFoundException(
                    $"No se encontró el producto con Id {grupo.Key}.");

            if (cantidadTotal > producto.Stock)
                throw new InvalidOperationException(
                    $"No hay stock suficiente para el producto {producto.Nombre}.");

            detalles.Add(new DetallePedido(
                producto.Id,
                producto.Nombre,
                cantidadTotal,
                producto.Precio));
        }

        var pedido = new Pedido(detalles);

        await _repositorio.AgregarAsync(pedido);
        await _repositorio.GuardarCambiosAsync();

        return ConvertirADto(pedido);
    }

    public async Task<PedidoDto?> ObtenerPorIdAsync(Guid id)
    {
        if (id == Guid.Empty)
            throw new ArgumentException(
                "El identificador del pedido es obligatorio.",
                nameof(id));

        var pedido = await _repositorio.ObtenerPorIdAsync(id);

        return pedido is null
            ? null
            : ConvertirADto(pedido);
    }

    private static PedidoDto ConvertirADto(Pedido pedido)
    {
        var detalles = pedido.Detalles
            .Select(detalle => new DetallePedidoDto(
                detalle.ProductoId,
                detalle.NombreProducto,
                detalle.Cantidad,
                detalle.PrecioUnitario,
                detalle.Subtotal))
            .ToList();

        return new PedidoDto(
            pedido.Id,
            pedido.Fecha,
            pedido.Estado,
            pedido.Total,
            detalles);
    }
}
