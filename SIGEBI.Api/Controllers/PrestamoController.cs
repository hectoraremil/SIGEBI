using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Prestamo;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrestamoController : ControllerBase
    {
        private readonly IPrestamoService _prestamoService;

        public PrestamoController(IPrestamoService prestamoService)
        {
            _prestamoService = prestamoService;
        }

        [HttpGet("GetPrestamos")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<PrestamoModel>> result = await _prestamoService.GetAllPrestamosAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetPrestamoById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<PrestamoModel> result = await _prestamoService.GetPrestamoByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetPrestamosActivosByUsuario")]
        public async Task<IActionResult> GetPrestamosActivosByUsuario(int usuarioId)
        {
            ServiceResult<List<PrestamoModel>> result = await _prestamoService.GetPrestamosActivosPorUsuarioAsync(usuarioId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetPrestamosVencidos")]
        public async Task<IActionResult> GetPrestamosVencidos()
        {
            ServiceResult<List<PrestamoModel>> result = await _prestamoService.GetPrestamosVencidosAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SavePrestamo")]
        public async Task<IActionResult> Post(PrestamoAddDto prestamoAddDto)
        {
            ServiceResult<bool> result = await _prestamoService.CreatePrestamoAsync(prestamoAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdatePrestamo")]
        public async Task<IActionResult> Post(int id, PrestamoUpdateDto prestamoUpdateDto)
        {
            ServiceResult<bool> result = await _prestamoService.UpdatePrestamoAsync(id, prestamoUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("RenovarPrestamo")]
        public async Task<IActionResult> Renovar(PrestamoRenovarDto prestamoRenovarDto)
        {
            ServiceResult<bool> result = await _prestamoService.RenovarPrestamoAsync(prestamoRenovarDto.Id, prestamoRenovarDto.DiasExtension);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeletePrestamo")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _prestamoService.DeletePrestamoAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
