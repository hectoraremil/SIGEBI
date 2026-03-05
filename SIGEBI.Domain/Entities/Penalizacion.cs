using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public class Penalizacion : AuditableEntity<int>
    {
        public int UsuarioId { get; set; }
        public Usuario? Usuario { get; set; }

        public int? PrestamoId { get; set; }
        public Prestamo? Prestamo { get; set; }

        public string Tipo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public EstadoPenalizacion Estado { get; set; }
        public bool Activo { get; set; } = true;

    }
}
