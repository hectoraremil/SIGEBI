namespace SIGEBI.Application.Dtos.Ejemplar
{
    public class EjemplarAddDto
    {
        public int RecursoBibliograficoId { get; set; }
        public string CodigoInventario { get; set; } = string.Empty;
        public string Ubicacion { get; set; } = string.Empty;
        public DateTime FechaAdquisicion { get; set; }
        public int CreadoPorUsuarioId { get; set; }
    }
}
