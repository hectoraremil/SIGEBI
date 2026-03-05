using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Models
{
    public class EjemplarModel
    {
        public int EjemplarId { get; set; }
        public string CodigoInventario { get; set; } = string.Empty;
        public EstadoEjemplar Estado { get; set; }
        public string Ubicacion { get; set; } = string.Empty;
        public int RecursoBibliograficoId { get; set; }
        public string TituloRecurso { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
    }
}
