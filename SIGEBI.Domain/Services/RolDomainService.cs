using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services.Interfaces;

namespace SIGEBI.Domain.Services
{
    public class RolDomainService : IRolDomainService
    {
        private readonly IRolRepository _rolRepository;

        public RolDomainService(IRolRepository rolRepository)
        {
            _rolRepository = rolRepository;
        }

        public async Task<Rol?> GetByIdAsync(int id)
        {
            return await _rolRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Rol>> GetAllAsync()
        {
            return await _rolRepository.GetAllAsync();
        }

        public async Task AddAsync(Rol rol)
        {
            await _rolRepository.AddAsync(rol);
        }

        public async Task UpdateAsync(Rol rol)
        {
            await _rolRepository.UpdateAsync(rol);
        }

        public async Task DeleteAsync(int id)
        {
            await _rolRepository.DeleteAsync(id);
        }
    }
}
