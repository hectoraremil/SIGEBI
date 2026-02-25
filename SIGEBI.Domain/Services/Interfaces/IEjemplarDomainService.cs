using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface IEjemplarDomainService
    {
        Task<Ejemplar?> GetByIdAsync(int id);
        Task<IEnumerable<Ejemplar>> GetByRecursoIdAsync(int recursoId);
        Task AddAsync(Ejemplar ejemplar);
        Task UpdateAsync(Ejemplar ejemplar);
        Task DeleteAsync(int id);
        Task<bool> EstaDisponibleAsync(int ejemplarId);
    }
}
