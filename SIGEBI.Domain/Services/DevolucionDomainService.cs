using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services.Interfaces;

namespace SIGEBI.Domain.Services
{
    public class DevolucionDomainService : IDevolucionDomainService
    {
        private readonly IDevolucionRepository _devolucionRepository;

        public DevolucionDomainService(IDevolucionRepository devolucionRepository)
        {
            _devolucionRepository = devolucionRepository;
        }

        public async Task<Devolucion?> GetByIdAsync(int id)
        {
            return await _devolucionRepository.GetByIdAsync(id);
        }

        public async Task<Devolucion?> GetByPrestamoIdAsync(int prestamoId)
        {
            return await _devolucionRepository.GetByPrestamoIdAsync(prestamoId);
        }

        public async Task RegistrarAsync(Devolucion devolucion)
        {
            await _devolucionRepository.AddAsync(devolucion);
        }

        public async Task UpdateAsync(Devolucion devolucion)
        {
            await _devolucionRepository.UpdateAsync(devolucion);
        }
    }
}
