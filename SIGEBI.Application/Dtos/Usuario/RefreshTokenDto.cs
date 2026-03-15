namespace SIGEBI.Application.Dtos.Usuario
{
    public class RefreshTokenDto
    {
        public int UsuarioId { get; set; }
        public string Email { get; set; } = string.Empty;
    }
}
