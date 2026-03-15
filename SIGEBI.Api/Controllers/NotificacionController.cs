using Microsoft.AspNetCore.Mvc;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Notificacion;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Models;

namespace SIGEBI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificacionController : ControllerBase
    {
        private readonly INotificacionService _notificacionService;

        public NotificacionController(INotificacionService notificacionService)
        {
            _notificacionService = notificacionService;
        }

        [HttpGet("GetNotificaciones")]
        public async Task<IActionResult> Get()
        {
            ServiceResult<List<NotificacionModel>> result = await _notificacionService.GetAllNotificacionesAsync();

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetNotificacionById")]
        public async Task<IActionResult> Get(int id)
        {
            ServiceResult<NotificacionModel> result = await _notificacionService.GetNotificacionByIdAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("GetNotificacionesByUsuario")]
        public async Task<IActionResult> GetByUsuario(int usuarioId)
        {
            ServiceResult<List<NotificacionModel>> result = await _notificacionService.GetNotificacionesPorUsuarioAsync(usuarioId);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("SaveNotificacion")]
        public async Task<IActionResult> Post(NotificacionAddDto notificacionAddDto)
        {
            ServiceResult<bool> result = await _notificacionService.CreateNotificacionAsync(notificacionAddDto);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("MarcarComoEnviada")]
        public async Task<IActionResult> MarcarComoEnviada(int id)
        {
            ServiceResult<bool> result = await _notificacionService.MarcarComoLeidaAsync(id);

            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
