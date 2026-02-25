using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services.Interfaces;

namespace SIGEBI.Domain.Services
{
    public class RecursoBibliograficoDomainService : IRecursoBibliograficoDomainService
    {
        private readonly IRecursoBibliograficoRepository _recursoRepository;

        public RecursoBibliograficoDomainService(IRecursoBibliograficoRepository recursoRepository)
        {
            _recursoRepository = recursoRepository;
        }

        public async Task<RecursoBibliografico?> GetByIdAsync(int id)
        {
            return await _recursoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<RecursoBibliografico>> GetAllAsync()
        {
            return await _recursoRepository.GetAllAsync();
        }

        public async Task AddAsync(RecursoBibliografico recurso)
        {
            await _recursoRepository.AddAsync(recurso);
        }

        public async Task UpdateAsync(RecursoBibliografico recurso)
        {
            await _recursoRepository.UpdateAsync(recurso);
        }

        public async Task DeleteAsync(int id)
        {
            await _recursoRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<RecursoBibliografico>> BuscarAsync(string termino)
        {
            return await _recursoRepository.GetByTituloAsync(termino);
        }
    }
}
