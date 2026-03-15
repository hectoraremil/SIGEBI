using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuditoriaController : ControllerBase
    {
        private readonly IAuditoriaService _auditoriaService;

        public AuditoriaController(IAuditoriaService auditoriaService)
        {
            _auditoriaService = auditoriaService;
        }

        [HttpGet("GetAuditorias")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<AuditoriaModel>> result = await _auditoriaService.GetAllAuditoriasAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetAuditoriaById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<AuditoriaModel> result = await _auditoriaService.GetAuditoriaByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetAuditoriasByEntidad")]
        public async Task<IActionResult> GetByEntidad(string entidad, int entidadId)
        {
            ServiceResult<List<AuditoriaModel>> result = await _auditoriaService.GetAuditoriasPorEntidadAsync(entidad, entidadId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
