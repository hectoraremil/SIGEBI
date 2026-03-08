namespace SIGEBI.Domain.Models
{
    public class DevolucionModel
    {
        public int Id { get; set; }
        public int PrestamoId { get; set; }
        public DateTime FechaDevolucion { get; set; }
        public int DiasAtraso { get; set; }
        public int RegistradoPorUsuarioId { get; set; }
        public string? Observaciones { get; set; }
        public bool Activo { get; set; }
    }
}
