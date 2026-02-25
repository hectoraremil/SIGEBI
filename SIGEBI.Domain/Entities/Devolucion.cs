using SIGEBI.Domain.Base;

namespace SIGEBI.Domain.Entities
{
    public class Devolucion : AuditableEntity<int>
    {
        public int PrestamoId { get; set; }
        public Prestamo? Prestamo { get; set; }

        public DateTime FechaDevolucion { get; set; }
        public int DiasAtraso { get; set; }

        public int RegistradoPorUsuarioId { get; set; }
        public Usuario? RegistradoPorUsuario { get; set; }

        public string? Observaciones { get; set; }
        public bool Activo { get; set; } = true;
    }
}
