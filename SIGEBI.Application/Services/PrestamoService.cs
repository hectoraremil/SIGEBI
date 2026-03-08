using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Prestamo;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Application.Services
{
    public sealed class PrestamoService : IPrestamoService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IPrestamoValidator _prestamoValidator;
        private readonly ILogger<PrestamoService> _logger;

        public PrestamoService(IPrestamoRepository prestamoRepository,
                              IPrestamoValidator prestamoValidator,
                              ILogger<PrestamoService> logger)
        {
            _prestamoRepository = prestamoRepository;
            _prestamoValidator = prestamoValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<List<PrestamoModel>>> GetAllPrestamosAsync()
        {
            ServiceResult<List<PrestamoModel>> serviceResult = new ServiceResult<List<PrestamoModel>>();
            
            try
            {
                _logger.LogInformation("Starting get all prestamos process.");
                
                var prestamos = await _prestamoRepository.GetAllAsync();
                
                var prestamosModel = prestamos.Select(p => new PrestamoModel
                {
                    Id = p.Id,
                    UsuarioId = p.UsuarioId,
                    EjemplarId = p.EjemplarId,
                    FechaPrestamo = p.FechaPrestamo,
                    FechaVencimiento = p.FechaVencimiento,
                    Estado = p.Estado,
                    RenovacionesRealizadas = p.RenovacionesRealizadas,
                    CreadoPorUsuarioId = p.CreadoPorUsuarioId,
                    Activo = p.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Prestamos retrieved successfully.";
                serviceResult.Data = prestamosModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all prestamos.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting prestamos.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<PrestamoModel>> GetPrestamoByIdAsync(int id)
        {
            ServiceResult<PrestamoModel> serviceResult = new ServiceResult<PrestamoModel>();
            
            try
            {
                _logger.LogInformation("Starting get prestamo by id process. Id: {Id}", id);
                
                var prestamo = await _prestamoRepository.GetByIdAsync(id);
                
                if (prestamo == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Prestamo not found.";
                    return serviceResult;
                }
                
                PrestamoModel prestamoModel = new PrestamoModel
                {
                    Id = prestamo.Id,
                    UsuarioId = prestamo.UsuarioId,
                    EjemplarId = prestamo.EjemplarId,
                    FechaPrestamo = prestamo.FechaPrestamo,
                    FechaVencimiento = prestamo.FechaVencimiento,
                    Estado = prestamo.Estado,
                    RenovacionesRealizadas = prestamo.RenovacionesRealizadas,
                    CreadoPorUsuarioId = prestamo.CreadoPorUsuarioId,
                    Activo = prestamo.Activo
                };
                
                serviceResult.Success = true;
                serviceResult.Message = "Prestamo retrieved successfully.";
                serviceResult.Data = prestamoModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting prestamo by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting prestamo.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CreatePrestamoAsync(PrestamoAddDto prestamoDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting prestamo creation process.");
            
            try
            {
                if (prestamoDto == null)
                {
                    _logger.LogWarning("Prestamo creation failed: PrestamoAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Prestamo data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                Domain.Entities.Prestamo prestamo = new Domain.Entities.Prestamo
                {
                    UsuarioId = prestamoDto.UsuarioId,
                    EjemplarId = prestamoDto.EjemplarId,
                    FechaPrestamo = prestamoDto.FechaPrestamo,
                    FechaVencimiento = prestamoDto.FechaVencimiento,
                    Estado = Domain.Enums.EstadoPrestamo.Activo,
                    RenovacionesRealizadas = 0,
                    CreadoPorUsuarioId = prestamoDto.CreadoPorUsuarioId,
                    Activo = true
                };
                
                _logger.LogInformation("Prestamo validation successful for: {@prestamo}", prestamo);
                
                await _prestamoRepository.AddAsync(prestamo);
                _logger.LogInformation("Prestamo added to repository successfully: {@prestamo}", prestamo);
                
                serviceResult.Success = true;
                serviceResult.Message = "Prestamo created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a prestamo.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a prestamo.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a prestamo.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> UpdatePrestamoAsync(int id, PrestamoUpdateDto prestamoDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting prestamo update process. Id: {Id}", id);
            
            try
            {
                var prestamo = await _prestamoRepository.GetByIdAsync(id);
                
                if (prestamo == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Prestamo not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                prestamo.RenovacionesRealizadas = prestamoDto.RenovacionesRealizadas;
                prestamo.Activo = prestamoDto.Activo;
                
                await _prestamoRepository.UpdateAsync(prestamo);
                
                serviceResult.Success = true;
                serviceResult.Message = "Prestamo updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating a prestamo.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a prestamo.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating a prestamo.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeletePrestamoAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting prestamo deletion process. Id: {Id}", id);
            
            try
            {
                var prestamo = await _prestamoRepository.GetByIdAsync(id);
                
                if (prestamo == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Prestamo not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                await _prestamoRepository.SoftDeleteAsync(id, 0);
                
                serviceResult.Success = true;
                serviceResult.Message = "Prestamo deleted successfully.";
                serviceResult.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a prestamo.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting a prestamo.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> RenovarPrestamoAsync(int id, int diasExtension)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting prestamo renewal process. Id: {Id}, DiasExtension: {Dias}", id, diasExtension);
            
            try
            {
                var prestamo = await _prestamoRepository.GetByIdAsync(id);
                
                if (prestamo == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Prestamo not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                if (prestamo.Estado != Domain.Enums.EstadoPrestamo.Activo)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Only active loans can be renewed.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                prestamo.FechaVencimiento = prestamo.FechaVencimiento.AddDays(diasExtension);
                prestamo.RenovacionesRealizadas = (prestamo.RenovacionesRealizadas ?? 0) + 1;
                
                await _prestamoRepository.UpdateAsync(prestamo);
                
                serviceResult.Success = true;
                serviceResult.Message = "Prestamo renewed successfully.";
                serviceResult.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while renewing a prestamo.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while renewing a prestamo.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<List<PrestamoModel>>> GetPrestamosActivosPorUsuarioAsync(int usuarioId)
        {
            ServiceResult<List<PrestamoModel>> serviceResult = new ServiceResult<List<PrestamoModel>>();
            
            try
            {
                _logger.LogInformation("Starting get prestamos activos by usuario process. UsuarioId: {UsuarioId}", usuarioId);
                
                var prestamos = await _prestamoRepository.GetActivosByUsuarioIdAsync(usuarioId);
                
                var prestamosModel = prestamos.Select(p => new PrestamoModel
                {
                    Id = p.Id,
                    PrestamoId = p.Id,
                    UsuarioId = p.UsuarioId,
                    EjemplarId = p.EjemplarId,
                    FechaPrestamo = p.FechaPrestamo,
                    FechaVencimiento = p.FechaVencimiento,
                    Estado = p.Estado,
                    RenovacionesRealizadas = p.RenovacionesRealizadas,
                    CreadoPorUsuarioId = p.CreadoPorUsuarioId,
                    Activo = p.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Prestamos retrieved successfully.";
                serviceResult.Data = prestamosModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting prestamos by usuario.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting prestamos.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<List<PrestamoModel>>> GetPrestamosVencidosAsync()
        {
            ServiceResult<List<PrestamoModel>> serviceResult = new ServiceResult<List<PrestamoModel>>();
            
            try
            {
                _logger.LogInformation("Starting get prestamos vencidos process.");
                
                var prestamos = await _prestamoRepository.GetVencidosAsync();
                
                var prestamosModel = prestamos.Select(p => new PrestamoModel
                {
                    Id = p.Id,
                    PrestamoId = p.Id,
                    UsuarioId = p.UsuarioId,
                    EjemplarId = p.EjemplarId,
                    FechaPrestamo = p.FechaPrestamo,
                    FechaVencimiento = p.FechaVencimiento,
                    Estado = p.Estado,
                    RenovacionesRealizadas = p.RenovacionesRealizadas,
                    CreadoPorUsuarioId = p.CreadoPorUsuarioId,
                    Activo = p.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Prestamos retrieved successfully.";
                serviceResult.Data = prestamosModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting prestamos vencidos.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting prestamos.";
            }
            
            return serviceResult;
        }
    }
}
