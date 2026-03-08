namespace SIGEBI.Application.Dtos.Devolucion
{
    public class DevolucionAddDto
    {
        public int PrestamoId { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public int DiasAtraso { get; set; }
        public string? Observaciones { get; set; }
        public int RegistradoPorUsuarioId { get; set; }
    }
}
