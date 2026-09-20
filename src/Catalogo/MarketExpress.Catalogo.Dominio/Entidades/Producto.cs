namespace MarketExpress.Catalogo.Dominio.Entidades;

public class Producto
{
    public Guid Id { get; private set; }
    public string Nombre { get; private set; }
    public decimal Precio { get; private set; }
    public int Stock { get; private set; }

    // Lo utilizará Entity Framework Core al recuperar productos de la base de datos.
    private Producto()
    {
        Nombre = string.Empty;
    }

    public Producto(string nombre, decimal precio, int stock)
    {
        if (string.IsNullOrWhiteSpace(nombre))
            throw new ArgumentException("El nombre del producto es obligatorio.");

        if (precio <= 0)
            throw new ArgumentException("El precio del producto debe ser mayor que cero.");

        if (stock < 0)
            throw new ArgumentException("El stock del producto no puede ser negativo.");

        Id = Guid.NewGuid();
        Nombre = nombre.Trim();
        Precio = precio;
        Stock = stock;
    }

    public void ReservarStock(int cantidad)
    {
        if (cantidad <= 0)
            throw new ArgumentException("La cantidad a reservar debe ser mayor que cero.");

        if (cantidad > Stock)
            throw new InvalidOperationException(
                "No se puede reservar una cantidad mayor al stock disponible.");

        Stock -= cantidad;
    }
}