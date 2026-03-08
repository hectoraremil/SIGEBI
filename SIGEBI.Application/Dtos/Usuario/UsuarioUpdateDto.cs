namespace SIGEBI.Application.Dtos.Usuario
{
    public class UsuarioUpdateDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int RolId { get; set; }
        public bool Activo { get; set; }
        public DateTime? BloqueadoHasta { get; set; }
    }
}
