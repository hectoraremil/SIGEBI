using SIGEBI.Domain.Enums;

namespace SIGEBI.Application.Dtos.Notificacion
{
    public class NotificacionAddDto
    {
        public int UsuarioId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Mensaje { get; set; } = string.Empty;
        public TipoNotificacion Tipo { get; set; }
        public int CreadoPorUsuarioId { get; set; }
    }
}
