using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface IPrestamoDomainService
    {
        Task<Prestamo?> GetByIdAsync(int id);
        Task<IEnumerable<Prestamo>> GetAllAsync();
        Task<IEnumerable<Prestamo>> GetActivosByUsuarioIdAsync(int usuarioId);
        Task RegistrarAsync(Prestamo prestamo);
        Task UpdateAsync(Prestamo prestamo);
        Task<bool> UsuarioPuedePrestarAsync(int usuarioId);
    }
}
