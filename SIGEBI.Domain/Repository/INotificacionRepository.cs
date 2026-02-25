using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface INotificacionRepository : IBaseRepository<Notificacion>
    {
        Task<IEnumerable<Notificacion>> GetByUsuarioIdAsync(int usuarioId);
    }
}
