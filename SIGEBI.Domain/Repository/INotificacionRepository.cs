using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface INotificacionRepository : IRepository<Notificacion, int>
    {
        Task<IReadOnlyList<Notificacion>> GetByUsuarioIdAsync(int usuarioId, CancellationToken ct = default);
    }
}
