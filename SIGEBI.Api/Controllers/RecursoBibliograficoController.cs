using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.RecursoBibliografico;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecursoBibliograficoController : ControllerBase
    {
        private readonly IRecursoBibliograficoService _recursoService;

        public RecursoBibliograficoController(IRecursoBibliograficoService recursoService)
        {
            _recursoService = recursoService;
        }

        [HttpGet("GetRecursos")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<RecursoBibliograficoModel>> result = await _recursoService.GetAllRecursosAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetRecursoById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<RecursoBibliograficoModel> result = await _recursoService.GetRecursoByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("SearchByTitulo")]
        public async Task<IActionResult> SearchByTitulo(string titulo)
        {
            ServiceResult<List<RecursoBibliograficoModel>> result = await _recursoService.BuscarPorTituloAsync(titulo);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("SearchByCategoria")]
        public async Task<IActionResult> SearchByCategoria(string categoria)
        {
            ServiceResult<List<RecursoBibliograficoModel>> result = await _recursoService.BuscarPorCategoriaAsync(categoria);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("SearchByAutor")]
        public async Task<IActionResult> SearchByAutor(string autor)
        {
            ServiceResult<List<RecursoBibliograficoModel>> result = await _recursoService.BuscarPorAutorAsync(autor);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveRecurso")]
        public async Task<IActionResult> Post(RecursoBibliograficoAddDto recursoAddDto)
        {
            ServiceResult<bool> result = await _recursoService.CreateRecursoAsync(recursoAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateRecurso")]
        public async Task<IActionResult> Post(int id, RecursoBibliograficoUpdateDto recursoUpdateDto)
        {
            ServiceResult<bool> result = await _recursoService.UpdateRecursoAsync(id, recursoUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteRecurso")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _recursoService.DeleteRecursoAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
