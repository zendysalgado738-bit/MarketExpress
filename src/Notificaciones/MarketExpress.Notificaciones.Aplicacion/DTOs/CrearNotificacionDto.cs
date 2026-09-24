namespace MarketExpress.Notificaciones.Aplicacion.DTOs;

public class CrearNotificacionDto
{
    public Guid PedidoId { get; set; }
    public string Destinatario { get; set; } = string.Empty;
    public string Mensaje { get; set; } = string.Empty;
}
