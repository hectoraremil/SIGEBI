using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Devolucion;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DevolucionController : ControllerBase
    {
        private readonly IDevolucionService _devolucionService;

        public DevolucionController(IDevolucionService devolucionService)
        {
            _devolucionService = devolucionService;
        }

        [HttpGet("GetDevoluciones")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<DevolucionModel>> result = await _devolucionService.GetAllDevolucionesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetDevolucionById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<DevolucionModel> result = await _devolucionService.GetDevolucionByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetDevolucionByPrestamoId")]
        public async Task<IActionResult> GetByPrestamoId(int prestamoId)
        {
            ServiceResult<DevolucionModel> result = await _devolucionService.GetDevolucionByPrestamoIdAsync(prestamoId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveDevolucion")]
        public async Task<IActionResult> Post(DevolucionAddDto devolucionAddDto)
        {
            ServiceResult<bool> result = await _devolucionService.CreateDevolucionAsync(devolucionAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
