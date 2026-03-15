using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Usuario;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Application.Services
{
    public sealed class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRolRepository _rolRepository;
        private readonly IUsuarioValidator _usuarioValidator;
        private readonly ILogger<UsuarioService> _logger;

        public UsuarioService(IUsuarioRepository usuarioRepository,
                             IRolRepository rolRepository,
                             IUsuarioValidator usuarioValidator,
                             ILogger<UsuarioService> logger)
        {
            _usuarioRepository = usuarioRepository;
            _rolRepository = rolRepository;
            _usuarioValidator = usuarioValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<List<UsuarioModel>>> GetAllUsuariosAsync()
        {
            ServiceResult<List<UsuarioModel>> serviceResult = new ServiceResult<List<UsuarioModel>>();
            
            try
            {
                _logger.LogInformation("Starting get all usuarios process.");
                
                var usuarios = await _usuarioRepository.GetAllAsync();
                
                var usuariosModel = usuarios.Select(u => new UsuarioModel
                {
                    Id = u.Id,
                    Nombre = u.Nombre,
                    Apellido = u.Apellido,
                    Email = u.Email,
                    RolId = u.RolId,
                    FechaRegistro = u.FechaRegistro,
                    Activo = u.Activo,
                    BloqueadoHasta = u.BloqueadoHasta
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Usuarios retrieved successfully.";
                serviceResult.Data = usuariosModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all usuarios.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting usuarios.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<UsuarioModel>> GetUsuarioByIdAsync(int id)
        {
            ServiceResult<UsuarioModel> serviceResult = new ServiceResult<UsuarioModel>();
            
            try
            {
                _logger.LogInformation("Starting get usuario by id process. Id: {Id}", id);
                
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                
                if (usuario == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Usuario not found.";
                    return serviceResult;
                }
                
                UsuarioModel usuarioModel = new UsuarioModel
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    RolId = usuario.RolId,
                    FechaRegistro = usuario.FechaRegistro,
                    Activo = usuario.Activo,
                    BloqueadoHasta = usuario.BloqueadoHasta
                };
                
                serviceResult.Success = true;
                serviceResult.Message = "Usuario retrieved successfully.";
                serviceResult.Data = usuarioModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting usuario by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting usuario.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CreateUsuarioAsync(UsuarioAddDto usuarioDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting usuario creation process.");
            
            try
            {
                if (usuarioDto == null)
                {
                    _logger.LogWarning("Usuario creation failed: UsuarioAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Usuario data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                Domain.Entities.Usuario usuario = new Domain.Entities.Usuario
                {
                    Nombre = usuarioDto.Nombre,
                    Apellido = usuarioDto.Apellido,
                    Email = usuarioDto.Email,
                    PasswordHash = usuarioDto.Password,
                    RolId = usuarioDto.RolId,
                    FechaRegistro = DateTime.Now,
                    Activo = true
                };
                
                _logger.LogInformation("Usuario validation successful for: {@usuario}", usuario);
                
                await _usuarioRepository.AddAsync(usuario);
                _logger.LogInformation("Usuario added to repository successfully: {@usuario}", usuario);
                
                serviceResult.Success = true;
                serviceResult.Message = "Usuario created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a usuario.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a usuario.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a usuario.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> UpdateUsuarioAsync(int id, UsuarioUpdateDto usuarioDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting usuario update process. Id: {Id}", id);
            
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                
                if (usuario == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Usuario not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                usuario.Nombre = usuarioDto.Nombre;
                usuario.Apellido = usuarioDto.Apellido;
                usuario.Email = usuarioDto.Email;
                usuario.RolId = usuarioDto.RolId;
                usuario.Activo = usuarioDto.Activo;
                usuario.BloqueadoHasta = usuarioDto.BloqueadoHasta;
                
                await _usuarioRepository.UpdateAsync(usuario);
                
                serviceResult.Success = true;
                serviceResult.Message = "Usuario updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating a usuario.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a usuario.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating a usuario.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteUsuarioAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting usuario deletion process. Id: {Id}", id);
            
            try
            {
                var usuario = await _usuarioRepository.GetByIdAsync(id);
                
                if (usuario == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Usuario not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                await _usuarioRepository.SoftDeleteAsync(id, 0);
                
                serviceResult.Success = true;
                serviceResult.Message = "Usuario deleted successfully.";
                serviceResult.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a usuario.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting a usuario.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<LoginResultModel>> LoginAsync(LoginDto loginDto)
        {
            ServiceResult<LoginResultModel> serviceResult = new ServiceResult<LoginResultModel>();
            
            _logger.LogInformation("Starting login process for email: {Email}", loginDto.Email);
            
            try
            {
                if (loginDto == null || string.IsNullOrEmpty(loginDto.Email) || string.IsNullOrEmpty(loginDto.Password))
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Email and password are required.";
                    return serviceResult;
                }
                
                var usuario = await _usuarioRepository.GetByEmailAsync(loginDto.Email);
                
                if (usuario == null)
                {
                    _logger.LogWarning("Login failed: User not found. Email: {Email}", loginDto.Email);
                    serviceResult.Success = false;
                    serviceResult.Message = "Invalid email or password.";
                    return serviceResult;
                }
                
                if (!usuario.Activo)
                {
                    _logger.LogWarning("Login failed: User is inactive. Email: {Email}", loginDto.Email);
                    serviceResult.Success = false;
                    serviceResult.Message = "User account is inactive.";
                    return serviceResult;
                }
                
                if (usuario.BloqueadoHasta.HasValue && usuario.BloqueadoHasta > DateTime.Now)
                {
                    _logger.LogWarning("Login failed: User is blocked until {BloqueadoHasta}", usuario.BloqueadoHasta);
                    serviceResult.Success = false;
                    serviceResult.Message = $"User account is blocked until {usuario.BloqueadoHasta}.";
                    return serviceResult;
                }
                
                if (usuario.PasswordHash != loginDto.Password)
                {
                    _logger.LogWarning("Login failed: Invalid password for email: {Email}", loginDto.Email);
                    serviceResult.Success = false;
                    serviceResult.Message = "Invalid email or password.";
                    return serviceResult;
                }
                
                LoginResultModel loginResult = new LoginResultModel
                {
                    Id = usuario.Id,
                    Nombre = usuario.Nombre,
                    Apellido = usuario.Apellido,
                    Email = usuario.Email,
                    RolId = usuario.RolId,
                    NombreRol = string.Empty,
                    Activo = usuario.Activo,
                    BloqueadoHasta = usuario.BloqueadoHasta
                };

                var rol = await _rolRepository.GetByIdAsync(usuario.RolId);

                if (rol != null)
                {
                    loginResult.NombreRol = rol.Nombre;
                }
                 
                _logger.LogInformation("Login successful for email: {Email}", loginDto.Email);
                serviceResult.Success = true;
                serviceResult.Message = "Login successful.";
                serviceResult.Data = loginResult;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred during login.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred during login.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CambiarPasswordAsync(CambiarPasswordDto cambiarPasswordDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting password change process for user ID: {UsuarioId}", cambiarPasswordDto.UsuarioId);
            
            try
            {
                if (cambiarPasswordDto == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Password change data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                var usuario = await _usuarioRepository.GetByIdAsync(cambiarPasswordDto.UsuarioId);
                
                if (usuario == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Usuario not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                if (usuario.PasswordHash != cambiarPasswordDto.PasswordActual)
                {
                    _logger.LogWarning("Password change failed: Current password is incorrect for user ID: {UsuarioId}", cambiarPasswordDto.UsuarioId);
                    serviceResult.Success = false;
                    serviceResult.Message = "Current password is incorrect.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                usuario.PasswordHash = cambiarPasswordDto.PasswordNuevo;
                await _usuarioRepository.UpdateAsync(usuario);
                
                _logger.LogInformation("Password changed successfully for user ID: {UsuarioId}", cambiarPasswordDto.UsuarioId);
                serviceResult.Success = true;
                serviceResult.Message = "Password changed successfully.";
                serviceResult.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while changing password.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while changing password.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }
    }
}
