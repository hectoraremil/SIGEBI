using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Models
{
    public class NotificacionModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Asunto { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;
        public TipoNotificacion Tipo { get; set; }
        public EstadoNotificacion Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEnvio { get; set; }
        public string Canal { get; set; } = string.Empty;
        public bool Activo { get; set; }
    }
}
