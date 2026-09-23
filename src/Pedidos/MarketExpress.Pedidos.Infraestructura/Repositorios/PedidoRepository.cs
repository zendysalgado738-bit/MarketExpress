using MarketExpress.Pedidos.Aplicacion.Interfaces;
using MarketExpress.Pedidos.Dominio.Entidades;
using MarketExpress.Pedidos.Infraestructura.Persistencia;
using Microsoft.EntityFrameworkCore;

namespace MarketExpress.Pedidos.Infraestructura.Repositorios;

public class PedidoRepository : IPedidoRepository
{
    private readonly PedidosDbContext _contexto;

    public PedidoRepository(PedidosDbContext contexto)
    {
        _contexto = contexto;
    }

    public async Task AgregarAsync(Pedido pedido)
    {
        await _contexto.Pedidos.AddAsync(pedido);
    }

    public async Task<Pedido?> ObtenerPorIdAsync(Guid id)
    {
        return await _contexto.Pedidos
            .Include(pedido => pedido.Detalles)
            .FirstOrDefaultAsync(pedido => pedido.Id == id);
    }

    public async Task GuardarCambiosAsync()
    {
        await _contexto.SaveChangesAsync();
    }
}