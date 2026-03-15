namespace SIGEBI.Domain.Models
{
    public class UsuarioMorosoModel
    {
        public int UsuarioId { get; set; }
        public string NombreCompleto { get; set; } = string.Empty;
        public int CantidadPrestamosVencidos { get; set; }
    }
}
