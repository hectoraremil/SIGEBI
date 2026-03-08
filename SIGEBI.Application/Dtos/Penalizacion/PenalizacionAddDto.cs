namespace SIGEBI.Application.Dtos.Penalizacion
{
    public class PenalizacionAddDto
    {
        public int UsuarioId { get; set; }
        public string TipoPenalizacion { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
        public int CreadoPorUsuarioId { get; set; }
    }
}
