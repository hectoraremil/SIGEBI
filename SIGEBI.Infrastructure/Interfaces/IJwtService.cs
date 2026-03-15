namespace SIGEBI.Infrastructure.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int usuarioId, string email, string nombreRol);
        DateTime GetExpirationDate();
    }
}
