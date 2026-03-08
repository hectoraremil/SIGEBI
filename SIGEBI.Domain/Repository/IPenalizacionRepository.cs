using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IPenalizacionRepository : IRepository<Penalizacion, int>
    {
        Task<IReadOnlyList<Penalizacion>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Penalizacion>> GetActivasByUsuarioIdAsync(int usuarioId, CancellationToken ct = default);
    }
}
