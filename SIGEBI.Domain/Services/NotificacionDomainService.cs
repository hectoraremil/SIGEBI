using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services.Interfaces;

namespace SIGEBI.Domain.Services
{
    public class NotificacionDomainService : INotificacionDomainService
    {
        private readonly INotificacionRepository _notificacionRepository;

        public NotificacionDomainService(INotificacionRepository notificacionRepository)
        {
            _notificacionRepository = notificacionRepository;
        }

        public async Task<Notificacion?> GetByIdAsync(int id)
        {
            return await _notificacionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Notificacion>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _notificacionRepository.GetByUsuarioIdAsync(usuarioId);
        }

        public async Task AddAsync(Notificacion notificacion)
        {
            await _notificacionRepository.AddAsync(notificacion);
        }

        public async Task UpdateAsync(Notificacion notificacion)
        {
            await _notificacionRepository.UpdateAsync(notificacion);
        }
    }
}
