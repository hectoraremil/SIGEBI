using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface IRolDomainService
    {
        Task<Rol?> GetByIdAsync(int id);
        Task<IEnumerable<Rol>> GetAllAsync();
        Task AddAsync(Rol rol);
        Task UpdateAsync(Rol rol);
        Task DeleteAsync(int id);
    }
}
