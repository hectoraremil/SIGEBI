using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public class Notificacion : AuditableEntity<int>
    {
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public TipoNotificacion Tipo { get; set; }
        public string Asunto { get; set; } = string.Empty;
        public string Contenido { get; set; } = string.Empty;

        public DateTime FechaCreacion { get; set; }
        public DateTime? FechaEnvio { get; set; }

        public EstadoNotificacion Estado { get; set; }
        public string Canal { get; set; } = string.Empty;
        public bool Activo { get; set; } = true;
    }
}
