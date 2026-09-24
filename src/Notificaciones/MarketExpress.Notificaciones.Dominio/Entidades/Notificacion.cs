namespace MarketExpress.Notificaciones.Dominio.Entidades;

public class Notificacion
{
    public Guid Id { get; private set; }
    public Guid PedidoId { get; private set; }
    public string Destinatario { get; private set; }
    public string Mensaje { get; private set; }
    public string Estado { get; private set; }
    public DateTime FechaCreacion { get; private set; }

    private Notificacion()
    {
        Destinatario = string.Empty;
        Mensaje = string.Empty;
        Estado = string.Empty;
    }

    public Notificacion(
        Guid pedidoId,
        string destinatario,
        string mensaje)
    {
        if (pedidoId == Guid.Empty)
        {
            throw new ArgumentException(
                "El identificador del pedido es obligatorio.",
                nameof(pedidoId));
        }

        if (string.IsNullOrWhiteSpace(destinatario))
        {
            throw new ArgumentException(
                "El destinatario es obligatorio.",
                nameof(destinatario));
        }

        if (destinatario.Trim().Length > 200)
        {
            throw new ArgumentException(
                "El destinatario no puede superar los 200 caracteres.",
                nameof(destinatario));
        }

        if (string.IsNullOrWhiteSpace(mensaje))
        {
            throw new ArgumentException(
                "El mensaje es obligatorio.",
                nameof(mensaje));
        }

        if (mensaje.Trim().Length > 500)
        {
            throw new ArgumentException(
                "El mensaje no puede superar los 500 caracteres.",
                nameof(mensaje));
        }

        Id = Guid.NewGuid();
        PedidoId = pedidoId;
        Destinatario = destinatario.Trim();
        Mensaje = mensaje.Trim();
        Estado = "Pendiente";
        FechaCreacion = DateTime.UtcNow;
    }
}
