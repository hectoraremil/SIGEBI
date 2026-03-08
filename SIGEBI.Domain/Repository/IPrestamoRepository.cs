using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IPrestamoRepository : IRepository<Prestamo, int>
    {
        Task<IReadOnlyList<Prestamo>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Prestamo>> GetActivosByUsuarioIdAsync(int usuarioId, CancellationToken ct = default);
        Task<IReadOnlyList<Prestamo>> GetVencidosAsync(CancellationToken ct = default);
    }
}
