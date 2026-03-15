namespace SIGEBI.Domain.Models
{
    public class LoginResultModel
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RolId { get; set; }
        public string NombreRol { get; set; } = string.Empty;
        public bool Activo { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime? ExpiraEn { get; set; }
    }
}
