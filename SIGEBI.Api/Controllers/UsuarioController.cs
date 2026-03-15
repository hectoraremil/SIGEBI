using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Usuario;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpGet("GetUsuarios")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<UsuarioModel>> result = await _usuarioService.GetAllUsuariosAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetUsuarioById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<UsuarioModel> result = await _usuarioService.GetUsuarioByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveUsuario")]
        public async Task<IActionResult> Post(UsuarioAddDto usuarioAddDto)
        {
            ServiceResult<bool> result = await _usuarioService.CreateUsuarioAsync(usuarioAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateUsuario")]
        public async Task<IActionResult> Post(int id, UsuarioUpdateDto usuarioUpdateDto)
        {
            ServiceResult<bool> result = await _usuarioService.UpdateUsuarioAsync(id, usuarioUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteUsuario")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _usuarioService.DeleteUsuarioAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
