namespace MarketExpress.Notificaciones.Aplicacion.DTOs;

public class NotificacionDto
{
    public Guid Id { get; set; }
    public Guid PedidoId { get; set; }
    public string Destinatario { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
    public string Estado { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}
