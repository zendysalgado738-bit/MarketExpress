using MarketExpress.Catalogo.Aplicacion.DTOs;
using MarketExpress.Catalogo.Aplicacion.Interfaces;
using MarketExpress.Catalogo.Dominio.Entidades;

namespace MarketExpress.Catalogo.Aplicacion.Servicios;

public class ProductoService : IProductoService
{
    private readonly IProductoRepository _repositorio;

    public ProductoService(IProductoRepository repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<ProductoDto> CrearAsync(CrearProductoDto datos)
    {
        var producto = new Producto(datos.Nombre, datos.Precio, datos.Stock);

        await _repositorio.AgregarAsync(producto);
        await _repositorio.GuardarCambiosAsync();

        return ConvertirADto(producto);
    }

    public async Task<IReadOnlyList<ProductoDto>> ListarAsync()
    {
        var productos = await _repositorio.ListarAsync();
        return productos.Select(ConvertirADto).ToList();
    }

    public async Task<ProductoDto?> ObtenerPorIdAsync(Guid id)
    {
        var producto = await _repositorio.ObtenerPorIdAsync(id);
        return producto is null ? null : ConvertirADto(producto);
    }

    public async Task<ProductoDto?> ReservarStockAsync(Guid id, int cantidad)
    {
        var producto = await _repositorio.ObtenerPorIdAsync(id);

        if (producto is null)
            return null;

        producto.ReservarStock(cantidad);
        await _repositorio.GuardarCambiosAsync();

        return ConvertirADto(producto);
    }

    private static ProductoDto ConvertirADto(Producto producto)
    {
        return new ProductoDto(
            producto.Id,
            producto.Nombre,
            producto.Precio,
            producto.Stock);
    }
}