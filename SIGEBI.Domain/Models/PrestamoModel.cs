using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Models
{
    public class PrestamoModel
    {
        public int PrestamoId { get; set; }
        public int UsuarioId { get; set; }
        public string NombreUsuario { get; set; } = string.Empty;
        public int EjemplarId { get; set; }
        public string CodigoInventario { get; set; } = string.Empty;
        public string TituloRecurso { get; set; } = string.Empty;
        public DateTime FechaPrestamo { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public EstadoPrestamo Estado { get; set; }
    }
}
