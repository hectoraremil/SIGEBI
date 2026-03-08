using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Services
{
    public sealed class AuditoriaService : IAuditoriaService
    {
        private readonly IAuditoriaRepository _auditoriaRepository;
        private readonly ILogger<AuditoriaService> _logger;

        public AuditoriaService(IAuditoriaRepository auditoriaRepository,
                                ILogger<AuditoriaService> logger)
        {
            _auditoriaRepository = auditoriaRepository;
            _logger = logger;
        }

        public async Task<ServiceResult<List<AuditoriaModel>>> GetAllAuditoriasAsync()
        {
            ServiceResult<List<AuditoriaModel>> serviceResult = new ServiceResult<List<AuditoriaModel>>();
            
            try
            {
                _logger.LogInformation("Starting get all auditorias process.");
                
                var auditorias = await _auditoriaRepository.GetAllAsync();
                
                var auditoriasModel = auditorias.Select(a => new AuditoriaModel
                {
                    Id = a.Id,
                    UsuarioId = a.UsuarioId,
                    Accion = a.Accion,
                    Entidad = a.Entidad,
                    EntidadId = a.EntidadId,
                    DatosAnteriores = a.DatosAnteriores,
                    DatosNuevos = a.DatosNuevos,
                    Fecha = a.Fecha,
                    IP = a.IP,
                    UserAgent = a.UserAgent,
                    Activo = a.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Auditorias retrieved successfully.";
                serviceResult.Data = auditoriasModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all auditorias.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting auditorias.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<AuditoriaModel>> GetAuditoriaByIdAsync(int id)
        {
            ServiceResult<AuditoriaModel> serviceResult = new ServiceResult<AuditoriaModel>();
            
            try
            {
                _logger.LogInformation("Starting get auditoria by id process. Id: {Id}", id);
                
                var auditoria = await _auditoriaRepository.GetByIdAsync(id);
                
                if (auditoria == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Auditoria not found.";
                    return serviceResult;
                }
                
                AuditoriaModel auditoriaModel = new AuditoriaModel
                {
                    Id = auditoria.Id,
                    UsuarioId = auditoria.UsuarioId,
                    Accion = auditoria.Accion,
                    Entidad = auditoria.Entidad,
                    EntidadId = auditoria.EntidadId,
                    DatosAnteriores = auditoria.DatosAnteriores,
                    DatosNuevos = auditoria.DatosNuevos,
                    Fecha = auditoria.Fecha,
                    IP = auditoria.IP,
                    UserAgent = auditoria.UserAgent,
                    Activo = auditoria.Activo
                };
                
                serviceResult.Success = true;
                serviceResult.Message = "Auditoria retrieved successfully.";
                serviceResult.Data = auditoriaModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting auditoria by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting auditoria.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<List<AuditoriaModel>>> GetAuditoriasPorEntidadAsync(string entidad, int entidadId)
        {
            ServiceResult<List<AuditoriaModel>> serviceResult = new ServiceResult<List<AuditoriaModel>>();
            
            try
            {
                _logger.LogInformation("Starting get auditorias by entidad process. Entidad: {Entidad}, EntidadId: {EntidadId}", entidad, entidadId);
                
                var auditorias = await _auditoriaRepository.GetByEntidadAsync(entidad, entidadId.ToString());
                
                var auditoriasModel = auditorias.Select(a => new AuditoriaModel
                {
                    Id = a.Id,
                    UsuarioId = a.UsuarioId,
                    Accion = a.Accion,
                    Entidad = a.Entidad,
                    EntidadId = a.EntidadId,
                    DatosAnteriores = a.DatosAnteriores,
                    DatosNuevos = a.DatosNuevos,
                    Fecha = a.Fecha,
                    IP = a.IP,
                    UserAgent = a.UserAgent,
                    Activo = a.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Auditorias retrieved successfully.";
                serviceResult.Data = auditoriasModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting auditorias by entidad.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting auditorias.";
            }
            
            return serviceResult;
        }
    }
}
