using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Ejemplar;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EjemplarController : ControllerBase
    {
        private readonly IEjemplarService _ejemplarService;

        public EjemplarController(IEjemplarService ejemplarService)
        {
            _ejemplarService = ejemplarService;
        }

        [HttpGet("GetEjemplares")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<EjemplarModel>> result = await _ejemplarService.GetAllEjemplaresAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetEjemplarById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<EjemplarModel> result = await _ejemplarService.GetEjemplarByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetEjemplaresByRecurso")]
        public async Task<IActionResult> GetByRecurso(int recursoId)
        {
            ServiceResult<List<EjemplarModel>> result = await _ejemplarService.GetEjemplaresPorRecursoAsync(recursoId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetEjemplaresDisponiblesByRecurso")]
        public async Task<IActionResult> GetDisponibles(int recursoId)
        {
            ServiceResult<List<EjemplarModel>> result = await _ejemplarService.GetEjemplaresDisponiblesPorRecursoAsync(recursoId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveEjemplar")]
        public async Task<IActionResult> Post(EjemplarAddDto ejemplarAddDto)
        {
            ServiceResult<bool> result = await _ejemplarService.CreateEjemplarAsync(ejemplarAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateEjemplar")]
        public async Task<IActionResult> Post(int id, EjemplarUpdateDto ejemplarUpdateDto)
        {
            ServiceResult<bool> result = await _ejemplarService.UpdateEjemplarAsync(id, ejemplarUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteEjemplar")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _ejemplarService.DeleteEjemplarAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
