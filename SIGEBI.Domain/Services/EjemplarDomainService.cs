using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services.Interfaces;

namespace SIGEBI.Domain.Services
{
    public class EjemplarDomainService : IEjemplarDomainService
    {
        private readonly IEjemplarRepository _ejemplarRepository;

        public EjemplarDomainService(IEjemplarRepository ejemplarRepository)
        {
            _ejemplarRepository = ejemplarRepository;
        }

        public async Task<Ejemplar?> GetByIdAsync(int id)
        {
            return await _ejemplarRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Ejemplar>> GetByRecursoIdAsync(int recursoId)
        {
            return await _ejemplarRepository.GetByRecursoIdAsync(recursoId);
        }

        public async Task AddAsync(Ejemplar ejemplar)
        {
            await _ejemplarRepository.AddAsync(ejemplar);
        }

        public async Task UpdateAsync(Ejemplar ejemplar)
        {
            await _ejemplarRepository.UpdateAsync(ejemplar);
        }

        public async Task DeleteAsync(int id)
        {
            await _ejemplarRepository.DeleteAsync(id);
        }

        public async Task<bool> EstaDisponibleAsync(int ejemplarId)
        {
            var ejemplar = await _ejemplarRepository.GetByIdAsync(ejemplarId);
            if (ejemplar == null) return false;
            return ejemplar.EstaDisponible();
        }
    }
}
