using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IRolRepository : IRepository<Rol, int>
    {
        Task<IReadOnlyList<Rol>> GetAllActiveAsync(CancellationToken ct = default);
    }
}
