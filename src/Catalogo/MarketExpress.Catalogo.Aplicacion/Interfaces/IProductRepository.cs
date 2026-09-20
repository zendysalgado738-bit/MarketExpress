using MarketExpress.Catalogo.Dominio.Entidades;

namespace MarketExpress.Catalogo.Aplicacion.Interfaces;

public interface IProductoRepository
{
    Task AgregarAsync(Producto producto);
    Task<IReadOnlyList<Producto>> ListarAsync();
    Task<Producto?> ObtenerPorIdAsync(Guid id);
    Task GuardarCambiosAsync();
}