using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Notificacion;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Application.Services
{
    public sealed class NotificacionService : INotificacionService
    {
        private readonly INotificacionRepository _notificacionRepository;
        private readonly INotificacionValidator _notificacionValidator;
        private readonly ILogger<NotificacionService> _logger;

        public NotificacionService(INotificacionRepository notificacionRepository,
                                   INotificacionValidator notificacionValidator,
                                   ILogger<NotificacionService> logger)
        {
            _notificacionRepository = notificacionRepository;
            _notificacionValidator = notificacionValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<List<NotificacionModel>>> GetAllNotificacionesAsync()
        {
            ServiceResult<List<NotificacionModel>> serviceResult = new ServiceResult<List<NotificacionModel>>();
            
            try
            {
                _logger.LogInformation("Starting get all notificaciones process.");
                
                var notificaciones = await _notificacionRepository.GetAllAsync();
                
                var notificacionesModel = notificaciones.Select(n => new NotificacionModel
                {
                    Id = n.Id,
                    UsuarioId = n.UsuarioId,
                    Asunto = n.Asunto,
                    Contenido = n.Contenido,
                    Tipo = n.Tipo,
                    Estado = n.Estado,
                    FechaCreacion = n.FechaCreacion,
                    FechaEnvio = n.FechaEnvio,
                    Canal = n.Canal,
                    Activo = n.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Notificaciones retrieved successfully.";
                serviceResult.Data = notificacionesModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all notificaciones.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting notificaciones.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<NotificacionModel>> GetNotificacionByIdAsync(int id)
        {
            ServiceResult<NotificacionModel> serviceResult = new ServiceResult<NotificacionModel>();
            
            try
            {
                _logger.LogInformation("Starting get notificacion by id process. Id: {Id}", id);
                
                var notificacion = await _notificacionRepository.GetByIdAsync(id);
                
                if (notificacion == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Notificacion not found.";
                    return serviceResult;
                }
                
                NotificacionModel notificacionModel = new NotificacionModel
                {
                    Id = notificacion.Id,
                    UsuarioId = notificacion.UsuarioId,
                    Asunto = notificacion.Asunto,
                    Contenido = notificacion.Contenido,
                    Tipo = notificacion.Tipo,
                    Estado = notificacion.Estado,
                    FechaCreacion = notificacion.FechaCreacion,
                    FechaEnvio = notificacion.FechaEnvio,
                    Canal = notificacion.Canal,
                    Activo = notificacion.Activo
                };
                
                serviceResult.Success = true;
                serviceResult.Message = "Notificacion retrieved successfully.";
                serviceResult.Data = notificacionModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting notificacion by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting notificacion.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CreateNotificacionAsync(NotificacionAddDto notificacionDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting notificacion creation process.");
            
            try
            {
                if (notificacionDto == null)
                {
                    _logger.LogWarning("Notificacion creation failed: NotificacionAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Notificacion data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                Domain.Entities.Notificacion notificacion = new Domain.Entities.Notificacion
                {
                    UsuarioId = notificacionDto.UsuarioId,
                    Asunto = notificacionDto.Titulo,
                    Contenido = notificacionDto.Mensaje,
                    Tipo = notificacionDto.Tipo,
                    Estado = EstadoNotificacion.Pendiente,
                    FechaCreacion = DateTime.Now,
                    Canal = "Sistema",
                    Activo = true
                };
                
                _logger.LogInformation("Notificacion validation successful for: {@notificacion}", notificacion);
                
                await _notificacionRepository.AddAsync(notificacion);
                _logger.LogInformation("Notificacion added to repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Notificacion created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a notificacion.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a notificacion.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a notificacion.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> MarcarComoLeidaAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting marcar notificacion como leida process. Id: {Id}", id);
            
            try
            {
                var notificacion = await _notificacionRepository.GetByIdAsync(id);
                
                if (notificacion == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Notificacion not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                notificacion.Estado = EstadoNotificacion.Enviada;
                notificacion.FechaEnvio = DateTime.Now;
                
                await _notificacionRepository.UpdateAsync(notificacion);
                
                serviceResult.Success = true;
                serviceResult.Message = "Notificacion marked as sent successfully.";
                serviceResult.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while marking notificacion as read.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while marking notificacion as read.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<List<NotificacionModel>>> GetNotificacionesPorUsuarioAsync(int usuarioId)
        {
            ServiceResult<List<NotificacionModel>> serviceResult = new ServiceResult<List<NotificacionModel>>();
            
            try
            {
                _logger.LogInformation("Starting get notificaciones by usuario process. UsuarioId: {UsuarioId}", usuarioId);
                
                var notificaciones = await _notificacionRepository.GetByUsuarioIdAsync(usuarioId);
                
                var notificacionesModel = notificaciones.Select(n => new NotificacionModel
                {
                    Id = n.Id,
                    UsuarioId = n.UsuarioId,
                    Asunto = n.Asunto,
                    Contenido = n.Contenido,
                    Tipo = n.Tipo,
                    Estado = n.Estado,
                    FechaCreacion = n.FechaCreacion,
                    FechaEnvio = n.FechaEnvio,
                    Canal = n.Canal,
                    Activo = n.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Notificaciones retrieved successfully.";
                serviceResult.Data = notificacionesModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting notificaciones by usuario.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting notificaciones.";
            }
            
            return serviceResult;
        }
    }
}
