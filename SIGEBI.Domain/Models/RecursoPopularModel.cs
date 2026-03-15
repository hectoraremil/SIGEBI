namespace SIGEBI.Domain.Models
{
    public class RecursoPopularModel
    {
        public int RecursoBibliograficoId { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public int CantidadPrestamos { get; set; }
    }
}
