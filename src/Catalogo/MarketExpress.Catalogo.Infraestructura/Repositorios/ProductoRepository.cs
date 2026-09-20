using MarketExpress.Catalogo.Aplicacion.Interfaces;
using MarketExpress.Catalogo.Dominio.Entidades;
using MarketExpress.Catalogo.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace MarketExpress.Catalogo.Infraestructura.Repositorios;

public class ProductoRepository : IProductoRepository
{
    private readonly CatalogoDbContext _contexto;

    public ProductoRepository(CatalogoDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task AgregarAsync(Producto producto)
    {
        await _contexto.Productos.AddAsync(producto);
    }

    public async Task<IReadOnlyList<Producto>> ListarAsync()
    {
        return await _contexto.Productos.AsNoTracking().ToListAsync();
    }

    public async Task<Producto?> ObtenerPorIdAsync(Guid id)
    {
        return await _contexto.Productos.FindAsync(id);
    }

    public async Task GuardarCambiosAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}