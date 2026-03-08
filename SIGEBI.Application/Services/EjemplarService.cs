using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Ejemplar;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Application.Services
{
    public sealed class EjemplarService : IEjemplarService
    {
        private readonly IEjemplarRepository _ejemplarRepository;
        private readonly IEjemplarValidator _ejemplarValidator;
        private readonly ILogger<EjemplarService> _logger;

        public EjemplarService(IEjemplarRepository ejemplarRepository,
                              IEjemplarValidator ejemplarValidator,
                              ILogger<EjemplarService> logger)
        {
            _ejemplarRepository = ejemplarRepository;
            _ejemplarValidator = ejemplarValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<List<EjemplarModel>>> GetAllEjemplaresAsync()
        {
            ServiceResult<List<EjemplarModel>> serviceResult = new ServiceResult<List<EjemplarModel>>();
            
            try
            {
                _logger.LogInformation("Starting get all ejemplares process.");
                
                var ejemplares = await _ejemplarRepository.GetAllAsync();
                
                var ejemplaresModel = ejemplares.Select(e => new EjemplarModel
                {
                    Id = e.Id,
                    RecursoBibliograficoId = e.RecursoBibliograficoId,
                    CodigoInventario = e.CodigoInventario,
                    Estado = e.Estado,
                    Ubicacion = e.Ubicacion,
                    FechaAdquisicion = e.FechaAdquisicion,
                    Activo = e.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Ejemplares retrieved successfully.";
                serviceResult.Data = ejemplaresModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all ejemplares.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting ejemplares.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<EjemplarModel>> GetEjemplarByIdAsync(int id)
        {
            ServiceResult<EjemplarModel> serviceResult = new ServiceResult<EjemplarModel>();
            
            try
            {
                _logger.LogInformation("Starting get ejemplar by id process. Id: {Id}", id);
                
                var ejemplar = await _ejemplarRepository.GetByIdAsync(id);
                
                if (ejemplar == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Ejemplar not found.";
                    return serviceResult;
                }
                
                EjemplarModel ejemplarModel = new EjemplarModel
                {
                    Id = ejemplar.Id,
                    RecursoBibliograficoId = ejemplar.RecursoBibliograficoId,
                    CodigoInventario = ejemplar.CodigoInventario,
                    Estado = ejemplar.Estado,
                    Ubicacion = ejemplar.Ubicacion,
                    FechaAdquisicion = ejemplar.FechaAdquisicion,
                    Activo = ejemplar.Activo
                };
                
                serviceResult.Success = true;
                serviceResult.Message = "Ejemplar retrieved successfully.";
                serviceResult.Data = ejemplarModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting ejemplar by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting ejemplar.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CreateEjemplarAsync(EjemplarAddDto ejemplarDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting ejemplar creation process.");
            
            try
            {
                if (ejemplarDto == null)
                {
                    _logger.LogWarning("Ejemplar creation failed: EjemplarAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Ejemplar data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                Domain.Entities.Ejemplar ejemplar = new Domain.Entities.Ejemplar
                {
                    RecursoBibliograficoId = ejemplarDto.RecursoBibliograficoId,
                    CodigoInventario = ejemplarDto.CodigoInventario,
                    Ubicacion = ejemplarDto.Ubicacion,
                    FechaAdquisicion = ejemplarDto.FechaAdquisicion,
                    Estado = Domain.Enums.EstadoEjemplar.Disponible,
                    Activo = true
                };
                
                _logger.LogInformation("Ejemplar validation successful for: {@ejemplar}", ejemplar);
                
                await _ejemplarRepository.AddAsync(ejemplar);
                _logger.LogInformation("Ejemplar added to repository successfully: {@ejemplar}", ejemplar);
                
                serviceResult.Success = true;
                serviceResult.Message = "Ejemplar created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a ejemplar.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a ejemplar.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a ejemplar.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> UpdateEjemplarAsync(int id, EjemplarUpdateDto ejemplarDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting ejemplar update process. Id: {Id}", id);
            
            try
            {
                var ejemplar = await _ejemplarRepository.GetByIdAsync(id);
                
                if (ejemplar == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Ejemplar not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                ejemplar.Ubicacion = ejemplarDto.Ubicacion;
                ejemplar.Activo = ejemplarDto.Activo;
                
                await _ejemplarRepository.UpdateAsync(ejemplar);
                
                serviceResult.Success = true;
                serviceResult.Message = "Ejemplar updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating a ejemplar.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a ejemplar.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating a ejemplar.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteEjemplarAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting ejemplar deletion process. Id: {Id}", id);
            
            try
            {
                var ejemplar = await _ejemplarRepository.GetByIdAsync(id);
                
                if (ejemplar == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Ejemplar not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                await _ejemplarRepository.SoftDeleteAsync(id, 0);
                
                serviceResult.Success = true;
                serviceResult.Message = "Ejemplar deleted successfully.";
                serviceResult.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a ejemplar.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting a ejemplar.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }
    }
}
