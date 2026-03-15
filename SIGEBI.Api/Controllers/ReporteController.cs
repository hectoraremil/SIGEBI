using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReporteController : ControllerBase
    {
        private readonly IReporteService _reporteService;

        public ReporteController(IReporteService reporteService)
        {
            _reporteService = reporteService;
        }

        [HttpGet("GetReportePrestamos")]
        public async Task<IActionResult> GetReportePrestamos()
        {
            ServiceResult<List<PrestamoModel>> result = await _reporteService.GetReportePrestamosAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetRecursosPopulares")]
        public async Task<IActionResult> GetRecursosPopulares()
        {
            ServiceResult<List<RecursoPopularModel>> result = await _reporteService.GetRecursosPopularesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetUsuariosMorosos")]
        public async Task<IActionResult> GetUsuariosMorosos()
        {
            ServiceResult<List<UsuarioMorosoModel>> result = await _reporteService.GetUsuariosMorososAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetEstadisticasGenerales")]
        public async Task<IActionResult> GetEstadisticasGenerales()
        {
            ServiceResult<EstadisticaGeneralModel> result = await _reporteService.GetEstadisticasGeneralesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
