namespace MarketExpress.Catalogo.Aplicacion.DTOs;

public record ProductoDto(Guid Id, string Nombre, decimal Precio, int Stock);