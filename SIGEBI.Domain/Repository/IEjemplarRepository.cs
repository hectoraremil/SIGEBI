using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IEjemplarRepository : IBaseRepository<Ejemplar>
    {
        Task<IEnumerable<Ejemplar>> GetByRecursoIdAsync(int recursoId);
        Task<IEnumerable<Ejemplar>> GetDisponiblesByRecursoIdAsync(int recursoId);
    }
}
