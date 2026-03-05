using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public class Prestamo : AuditableEntity<int>
    {
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int EjemplarId { get; set; }
        public Ejemplar? Ejemplar { get; set; }

        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public EstadoPrestamo Estado { get; set; }

        public int? RenovacionesRealizadas { get; set; }
        public int? CreadoPorUsuarioId { get; set; }
        public bool Activo { get; set; } = true;

    }
}
