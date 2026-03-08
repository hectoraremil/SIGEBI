using SIGEBI.Domain.Enums;

namespace SIGEBI.Domain.Models
{
    public class PenalizacionModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int? PrestamoId { get; set; }
        public string Tipo { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public EstadoPenalizacion Estado { get; set; }
        public bool Activo { get; set; }
    }
}
