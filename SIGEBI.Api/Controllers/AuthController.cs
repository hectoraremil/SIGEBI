using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Usuario;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;
using SIGEBI.Infrastructure.Interfaces;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IRolService _rolService;
        private readonly IJwtService _jwtService;

        public AuthController(IUsuarioService usuarioService,
                              IRolService rolService,
                              IJwtService jwtService)
        {
            _usuarioService = usuarioService;
            _rolService = rolService;
            _jwtService = jwtService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Post(LoginDto loginDto)
        {
            ServiceResult<LoginResultModel> result = await _usuarioService.LoginAsync(loginDto);

            if (result.Success && result.Data != null)
            {
                if (string.IsNullOrEmpty(result.Data.NombreRol))
                {
                    ServiceResult<RolModel> rolResult = await _rolService.GetRolByIdAsync(result.Data.RolId);

                    if (rolResult.Success && rolResult.Data != null)
                    {
                        result.Data.NombreRol = rolResult.Data.Nombre;
                    }
                }

                result.Data.Token = _jwtService.GenerateToken(result.Data.Id, result.Data.Email, result.Data.NombreRol);
                result.Data.ExpiraEn = _jwtService.GetExpirationDate();
            }

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Logout")]
        public IActionResult Post()
        {
            ServiceResult<bool> result = new ServiceResult<bool>();
            result.Success = true;
            result.Message = "Logout successful.";
            result.Data = true;

            return Ok(result);
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> Refresh(RefreshTokenDto refreshTokenDto)
        {
            ServiceResult<UsuarioModel> usuarioResult = await _usuarioService.GetUsuarioByIdAsync(refreshTokenDto.UsuarioId);

            if (!usuarioResult.Success || usuarioResult.Data == null)
            {
                return BadRequest(usuarioResult);
            }

            if (usuarioResult.Data.Email != refreshTokenDto.Email)
            {
                ServiceResult<LoginResultModel> invalidResult = new ServiceResult<LoginResultModel>();
                invalidResult.Success = false;
                invalidResult.Message = "Invalid refresh data.";
                return BadRequest(invalidResult);
            }

            ServiceResult<RolModel> rolResult = await _rolService.GetRolByIdAsync(usuarioResult.Data.RolId);

            if (!rolResult.Success || rolResult.Data == null)
            {
                return BadRequest(rolResult);
            }

            ServiceResult<LoginResultModel> result = new ServiceResult<LoginResultModel>();
            result.Success = true;
            result.Message = "Token refreshed successfully.";
            result.Data = new LoginResultModel
            {
                Id = usuarioResult.Data.Id,
                Nombre = usuarioResult.Data.Nombre,
                Apellido = usuarioResult.Data.Apellido,
                Email = usuarioResult.Data.Email,
                RolId = usuarioResult.Data.RolId,
                NombreRol = rolResult.Data.Nombre,
                Activo = usuarioResult.Data.Activo,
                BloqueadoHasta = usuarioResult.Data.BloqueadoHasta,
                Token = _jwtService.GenerateToken(usuarioResult.Data.Id, usuarioResult.Data.Email, rolResult.Data.Nombre),
                ExpiraEn = _jwtService.GetExpirationDate()
            };

            return Ok(result);
        }

        [HttpPost("CambiarPassword")]
        public async Task<IActionResult> ChangePassword(CambiarPasswordDto cambiarPasswordDto)
        {
            ServiceResult<bool> result = await _usuarioService.CambiarPasswordAsync(cambiarPasswordDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
