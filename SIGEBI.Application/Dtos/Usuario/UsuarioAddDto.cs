namespace SIGEBI.Application.Dtos.Usuario
{
    public class UsuarioAddDto
    {
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public int RolId { get; set; }
        public int CreadoPorUsuarioId { get; set; }
    }
}
