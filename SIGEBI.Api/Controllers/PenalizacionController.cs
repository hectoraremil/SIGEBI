using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Penalizacion;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PenalizacionController : ControllerBase
    {
        private readonly IPenalizacionService _penalizacionService;

        public PenalizacionController(IPenalizacionService penalizacionService)
        {
            _penalizacionService = penalizacionService;
        }

        [HttpGet("GetPenalizaciones")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<PenalizacionModel>> result = await _penalizacionService.GetAllPenalizacionesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetPenalizacionById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<PenalizacionModel> result = await _penalizacionService.GetPenalizacionByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetPenalizacionesByUsuario")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            ServiceResult<List<PenalizacionModel>> result = await _penalizacionService.GetPenalizacionesPorUsuarioAsync(usuarioId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SavePenalizacion")]
        public async Task<IActionResult> Post(PenalizacionAddDto penalizacionAddDto)
        {
            ServiceResult<bool> result = await _penalizacionService.CreatePenalizacionAsync(penalizacionAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("LevantarPenalizacion")]
        public async Task<IActionResult> Levantar(int id)
        {
            ServiceResult<bool> result = await _penalizacionService.LevantarPenalizacionAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
