namespace SIGEBI.Application.Dtos.Prestamo
{
    public class PrestamoAddDto
    {
        public int UsuarioId { get; set; }
        public int EjemplarId { get; set; }
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CreadoPorUsuarioId { get; set; }
    }
}
