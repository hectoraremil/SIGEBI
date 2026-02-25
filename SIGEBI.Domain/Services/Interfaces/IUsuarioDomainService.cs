using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface IUsuarioDomainService
    {
        Task<Usuario?> GetByIdAsync(int id);
        Task<IEnumerable<Usuario>> GetAllAsync();
        Task AddAsync(Usuario usuario);
        Task UpdateAsync(Usuario usuario);
        Task DeleteAsync(int id);
        Task<Usuario?> GetByEmailAsync(string email);
    }
}
