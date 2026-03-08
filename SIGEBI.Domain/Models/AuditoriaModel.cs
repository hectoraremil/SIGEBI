namespace SIGEBI.Domain.Models
{
    public class AuditoriaModel
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public string Accion { get; set; } = string.Empty;
        public string Entidad { get; set; } = string.Empty;
        public string EntidadId { get; set; } = string.Empty;
        public string? DatosAnteriores { get; set; }
        public string? DatosNuevos { get; set; }
        public DateTime Fecha { get; set; }
        public string? IP { get; set; }
        public string? UserAgent { get; set; }
        public bool Activo { get; set; }
    }
}
