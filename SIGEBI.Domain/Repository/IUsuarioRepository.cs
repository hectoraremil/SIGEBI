using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IUsuarioRepository : IRepository<Usuario, int>
    {
        Task<IReadOnlyList<Usuario>> GetAllAsync(CancellationToken ct = default);
        Task<IReadOnlyList<Usuario>> GetAllActiveAsync(CancellationToken ct = default);
        Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct = default);
    }
}
