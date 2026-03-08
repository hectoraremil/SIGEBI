using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Devolucion;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Application.Services
{
    public sealed class DevolucionService : IDevolucionService
    {
        private readonly IDevolucionRepository _devolucionRepository;
        private readonly IDevolucionValidator _devolucionValidator;
        private readonly ILogger<DevolucionService> _logger;

        public DevolucionService(IDevolucionRepository devolucionRepository,
                                IDevolucionValidator devolucionValidator,
                                ILogger<DevolucionService> logger)
        {
            _devolucionRepository = devolucionRepository;
            _devolucionValidator = devolucionValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<List<DevolucionModel>>> GetAllDevolucionesAsync()
        {
            ServiceResult<List<DevolucionModel>> serviceResult = new ServiceResult<List<DevolucionModel>>();
            
            try
            {
                _logger.LogInformation("Starting get all devoluciones process.");
                
                var devoluciones = await _devolucionRepository.GetAllAsync();
                
                var devolucionesModel = devoluciones.Select(d => new DevolucionModel
                {
                    Id = d.Id,
                    PrestamoId = d.PrestamoId,
                    FechaDevolucion = d.FechaDevolucion,
                    DiasAtraso = d.DiasAtraso,
                    RegistradoPorUsuarioId = d.RegistradoPorUsuarioId,
                    Observaciones = d.Observaciones,
                    Activo = d.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Devoluciones retrieved successfully.";
                serviceResult.Data = devolucionesModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all devoluciones.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting devoluciones.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<DevolucionModel>> GetDevolucionByIdAsync(int id)
        {
            ServiceResult<DevolucionModel> serviceResult = new ServiceResult<DevolucionModel>();
            
            try
            {
                _logger.LogInformation("Starting get devolucion by id process. Id: {Id}", id);
                
                var devolucion = await _devolucionRepository.GetByIdAsync(id);
                
                if (devolucion == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Devolucion not found.";
                    return serviceResult;
                }
                
                DevolucionModel devolucionModel = new DevolucionModel
                {
                    Id = devolucion.Id,
                    PrestamoId = devolucion.PrestamoId,
                    FechaDevolucion = devolucion.FechaDevolucion,
                    DiasAtraso = devolucion.DiasAtraso,
                    RegistradoPorUsuarioId = devolucion.RegistradoPorUsuarioId,
                    Observaciones = devolucion.Observaciones,
                    Activo = devolucion.Activo
                };
                
                serviceResult.Success = true;
                serviceResult.Message = "Devolucion retrieved successfully.";
                serviceResult.Data = devolucionModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting devolucion by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting devolucion.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CreateDevolucionAsync(DevolucionAddDto devolucionDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting devolucion creation process.");
            
            try
            {
                if (devolucionDto == null)
                {
                    _logger.LogWarning("Devolucion creation failed: DevolucionAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Devolucion data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                Domain.Entities.Devolucion devolucion = new Domain.Entities.Devolucion
                {
                    PrestamoId = devolucionDto.PrestamoId,
                    FechaDevolucion = devolucionDto.FechaDevolucion,
                    DiasAtraso = devolucionDto.DiasAtraso,
                    RegistradoPorUsuarioId = devolucionDto.RegistradoPorUsuarioId,
                    Observaciones = devolucionDto.Observaciones,
                    Activo = true
                };
                
                _logger.LogInformation("Devolucion validation successful for: {@devolucion}", devolucion);
                
                await _devolucionRepository.AddAsync(devolucion);
                _logger.LogInformation("Devolucion added to repository successfully: {@devolucion}", devolucion);
                
                serviceResult.Success = true;
                serviceResult.Message = "Devolucion created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a devolucion.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a devolucion.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a devolucion.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }
    }
}
