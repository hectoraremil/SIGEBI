using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Notificacion;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface INotificacionService
    {
        Task<ServiceResult<List<NotificacionModel>>> GetAllNotificacionesAsync();
        Task<ServiceResult<NotificacionModel>> GetNotificacionByIdAsync(int id);
        Task<ServiceResult<bool>> CreateNotificacionAsync(NotificacionAddDto notificacionDto);
        Task<ServiceResult<bool>> MarcarComoLeidaAsync(int id);
        Task<ServiceResult<List<NotificacionModel>>> GetNotificacionesPorUsuarioAsync(int usuarioId);
    }
}
