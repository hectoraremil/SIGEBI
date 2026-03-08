using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IDevolucionRepository : IRepository<Devolucion, int>
    {
        Task<IReadOnlyList<Devolucion>> GetAllAsync(CancellationToken ct = default);
        Task<Devolucion?> GetByPrestamoIdAsync(int prestamoId, CancellationToken ct = default);
    }
}
