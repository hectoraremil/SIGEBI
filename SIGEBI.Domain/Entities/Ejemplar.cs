using SIGEBI.Domain.Base;
using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Entities
{
    public class Ejemplar : AuditableEntity<int>
    {
        public int RecursoBibliograficoId { get; set; }
        public RecursoBibliografico? RecursoBibliografico { get; set; }

        public string CodigoInventario { get; set; } = string.Empty;
        public EstadoEjemplar Estado { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime FechaAdquisicion { get; set; }
        public bool Activo { get; set; } = true;

    }
}
