using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Penalizacion;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Application.Services
{
    public sealed class PenalizacionService : IPenalizacionService
    {
        private readonly IPenalizacionRepository _penalizacionRepository;
        private readonly IPenalizacionValidator _penalizacionValidator;
        private readonly ILogger<PenalizacionService> _logger;

        public PenalizacionService(IPenalizacionRepository penalizacionRepository,
                                   IPenalizacionValidator penalizacionValidator,
                                   ILogger<PenalizacionService> logger)
        {
            _penalizacionRepository = penalizacionRepository;
            _penalizacionValidator = penalizacionValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<List<PenalizacionModel>>> GetAllPenalizacionesAsync()
        {
            ServiceResult<List<PenalizacionModel>> serviceResult = new ServiceResult<List<PenalizacionModel>>();
            
            try
            {
                _logger.LogInformation("Starting get all penalizaciones process.");
                
                var penalizaciones = await _penalizacionRepository.GetAllAsync();
                
                var penalizacionesModel = penalizaciones.Select(p => new PenalizacionModel
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    PrestamoId = p.PrestamoId,
                    Tipo = p.Tipo,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin,
                    Motivo = p.Motivo,
                    Estado = p.Estado,
                    Activo = p.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Penalizaciones retrieved successfully.";
                serviceResult.Data = penalizacionesModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all penalizaciones.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting penalizaciones.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<PenalizacionModel>> GetPenalizacionByIdAsync(int id)
        {
            ServiceResult<PenalizacionModel> serviceResult = new ServiceResult<PenalizacionModel>();
            
            try
            {
                _logger.LogInformation("Starting get penalizacion by id process. Id: {Id}", id);
                
                var penalizacion = await _penalizacionRepository.GetByIdAsync(id);
                
                if (penalizacion == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Penalizacion not found.";
                    return serviceResult;
                }
                
                PenalizacionModel penalizacionModel = new PenalizacionModel
                {
                    Id = penalizacion.Id,
                    UsuarioId = penalizacion.UsuarioId,
                    PrestamoId = penalizacion.PrestamoId,
                    Tipo = penalizacion.Tipo,
                    FechaInicio = penalizacion.FechaInicio,
                    FechaFin = penalizacion.FechaFin,
                    Motivo = penalizacion.Motivo,
                    Estado = penalizacion.Estado,
                    Activo = penalizacion.Activo
                };
                
                serviceResult.Success = true;
                serviceResult.Message = "Penalizacion retrieved successfully.";
                serviceResult.Data = penalizacionModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting penalizacion by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting penalizacion.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CreatePenalizacionAsync(PenalizacionAddDto penalizacionDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting penalizacion creation process.");
            
            try
            {
                if (penalizacionDto == null)
                {
                    _logger.LogWarning("Penalizacion creation failed: PenalizacionAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Penalizacion data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                Domain.Entities.Penalizacion penalizacion = new Domain.Entities.Penalizacion
                {
                    UsuarioId = penalizacionDto.UsuarioId,
                    Tipo = penalizacionDto.TipoPenalizacion,
                    Motivo = penalizacionDto.Descripcion,
                    FechaInicio = penalizacionDto.FechaInicio,
                    FechaFin = penalizacionDto.FechaFin ?? DateTime.Now.AddDays(30),
                    Estado = Domain.Enums.EstadoPenalizacion.Activa,
                    Activo = true
                };
                
                _logger.LogInformation("Penalizacion validation successful for: {@penalizacion}", penalizacion);
                
                await _penalizacionRepository.AddAsync(penalizacion);
                _logger.LogInformation("Penalizacion added to repository successfully.");
                
                serviceResult.Success = true;
                serviceResult.Message = "Penalizacion created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a penalizacion.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a penalizacion.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a penalizacion.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<List<PenalizacionModel>>> GetPenalizacionesPorUsuarioAsync(int usuarioId)
        {
            ServiceResult<List<PenalizacionModel>> serviceResult = new ServiceResult<List<PenalizacionModel>>();
            
            try
            {
                _logger.LogInformation("Starting get penalizaciones by usuario process. UsuarioId: {UsuarioId}", usuarioId);
                
                var penalizaciones = await _penalizacionRepository.GetActivasByUsuarioIdAsync(usuarioId);
                
                var penalizacionesModel = penalizaciones.Select(p => new PenalizacionModel
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    PrestamoId = p.PrestamoId,
                    Tipo = p.Tipo,
                    FechaInicio = p.FechaInicio,
                    FechaFin = p.FechaFin,
                    Motivo = p.Motivo,
                    Estado = p.Estado,
                    Activo = p.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Penalizaciones retrieved successfully.";
                serviceResult.Data = penalizacionesModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting penalizaciones by usuario.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting penalizaciones.";
            }
            
            return serviceResult;
        }
    }
}
