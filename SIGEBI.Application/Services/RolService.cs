using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Rol;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Application.Services
{
    public sealed class RolService : IRolService
    {
        private readonly IRolRepository _rolRepository;
        private readonly IRolValidator _rolValidator;
        private readonly ILogger<RolService> _logger;

        public RolService(IRolRepository rolRepository,
                          IRolValidator rolValidator,
                          ILogger<RolService> logger)
        {
            _rolRepository = rolRepository;
            _rolValidator = rolValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<List<RolModel>>> GetRolesAsync()
        {
            ServiceResult<List<RolModel>> serviceResult = new ServiceResult<List<RolModel>>();

            try
            {
                var roles = await _rolRepository.GetAllActiveAsync();

                serviceResult.Success = true;
                serviceResult.Message = "Roles retrieved successfully.";
                serviceResult.Data = roles.Select(r => new RolModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Descripcion = r.Descripcion,
                    MaxPrestamos = r.MaxPrestamos,
                    DiasPrestamoDefault = r.DiasPrestamoDefault,
                    PuedeRenovar = r.PuedeRenovar,
                    MaxRenovaciones = r.MaxRenovaciones,
                    Activo = !r.Deleted
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting roles.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting roles.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<RolModel>> GetRolByIdAsync(int id)
        {
            ServiceResult<RolModel> serviceResult = new ServiceResult<RolModel>();

            try
            {
                var rol = await _rolRepository.GetByIdAsync(id);

                if (rol == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Rol not found.";
                    return serviceResult;
                }

                serviceResult.Success = true;
                serviceResult.Message = "Rol retrieved successfully.";
                serviceResult.Data = new RolModel
                {
                    Id = rol.Id,
                    Nombre = rol.Nombre,
                    Descripcion = rol.Descripcion,
                    MaxPrestamos = rol.MaxPrestamos,
                    DiasPrestamoDefault = rol.DiasPrestamoDefault,
                    PuedeRenovar = rol.PuedeRenovar,
                    MaxRenovaciones = rol.MaxRenovaciones,
                    Activo = !rol.Deleted
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting rol by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting rol.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CreateRolAsync(RolAddDto rolDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();

            try
            {
                Domain.Entities.Rol rol = new Domain.Entities.Rol
                {
                    Nombre = rolDto.Nombre,
                    Descripcion = rolDto.Descripcion,
                    MaxPrestamos = rolDto.MaxPrestamos,
                    DiasPrestamoDefault = rolDto.DiasPrestamoDefault,
                    PuedeRenovar = rolDto.PuedeRenovar,
                    MaxRenovaciones = rolDto.MaxRenovaciones
                };

                await _rolRepository.AddAsync(rol);

                serviceResult.Success = true;
                serviceResult.Message = "Rol created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a rol.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a rol.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a rol.";
                serviceResult.Data = false;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> UpdateRolAsync(int id, RolUpdateDto rolDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();

            try
            {
                var rol = await _rolRepository.GetByIdAsync(id);

                if (rol == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Rol not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }

                rol.Nombre = rolDto.Nombre;
                rol.Descripcion = rolDto.Descripcion;
                rol.MaxPrestamos = rolDto.MaxPrestamos;
                rol.DiasPrestamoDefault = rolDto.DiasPrestamoDefault;
                rol.PuedeRenovar = rolDto.PuedeRenovar;
                rol.MaxRenovaciones = rolDto.MaxRenovaciones;

                await _rolRepository.UpdateAsync(rol);

                serviceResult.Success = true;
                serviceResult.Message = "Rol updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating a rol.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a rol.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating a rol.";
                serviceResult.Data = false;
            }

            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteRolAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();

            try
            {
                var rol = await _rolRepository.GetByIdAsync(id);

                if (rol == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Rol not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }

                await _rolRepository.SoftDeleteAsync(id, 0);

                serviceResult.Success = true;
                serviceResult.Message = "Rol deleted successfully.";
                serviceResult.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a rol.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting a rol.";
                serviceResult.Data = false;
            }

            return serviceResult;
        }
    }
}
