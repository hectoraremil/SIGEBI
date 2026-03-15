using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Rol;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolController : ControllerBase
    {
        private readonly IRolService _rolService;

        public RolController(IRolService rolService)
        {
            _rolService = rolService;
        }

        [HttpGet("GetRoles")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<RolModel>> result = await _rolService.GetRolesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetRolById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<RolModel> result = await _rolService.GetRolByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveRol")]
        public async Task<IActionResult> Post(RolAddDto rolAddDto)
        {
            ServiceResult<bool> result = await _rolService.CreateRolAsync(rolAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("UpdateRol")]
        public async Task<IActionResult> Post(int id, RolUpdateDto rolUpdateDto)
        {
            ServiceResult<bool> result = await _rolService.UpdateRolAsync(id, rolUpdateDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("DeleteRol")]
        public async Task<IActionResult> Delete(int id)
        {
            ServiceResult<bool> result = await _rolService.DeleteRolAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
