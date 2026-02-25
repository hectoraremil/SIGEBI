using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface INotificacionDomainService
    {
        Task<Notificacion?> GetByIdAsync(int id);
        Task<IEnumerable<Notificacion>> GetByUsuarioIdAsync(int usuarioId);
        Task AddAsync(Notificacion notificacion);
        Task UpdateAsync(Notificacion notificacion);
    }
}
