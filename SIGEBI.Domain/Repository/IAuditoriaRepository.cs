using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IAuditoriaRepository : IRepository<Auditoria, int>
    {
        Task<IReadOnlyList<Auditoria>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Auditoria>> GetByEntidadAsync(string entidad, string entidadId, CancellationToken ct = default);
    }
}
