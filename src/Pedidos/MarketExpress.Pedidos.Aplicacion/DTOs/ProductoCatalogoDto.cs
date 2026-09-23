namespace MarketExpress.Pedidos.Aplicacion.DTOs;

public record ProductoCatalogoDto(
    Guid Id,
    string Nombre,
    decimal Precio,
    int Stock);
    