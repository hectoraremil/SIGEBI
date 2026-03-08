using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IEjemplarRepository : IRepository<Ejemplar, int>
    {
        Task<IReadOnlyList<Ejemplar>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Ejemplar>> GetByRecursoIdAsync(int recursoId, CancellationToken ct = default);
        Task<IReadOnlyList<Ejemplar>> GetDisponiblesByRecursoIdAsync(int recursoId, CancellationToken ct = default);
    }
}
