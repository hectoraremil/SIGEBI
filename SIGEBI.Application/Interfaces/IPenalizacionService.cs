using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Penalizacion;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IPenalizacionService
    {
        Task<ServiceResult<List<PenalizacionModel>>> GetAllPenalizacionesAsync();
        Task<ServiceResult<PenalizacionModel>> GetPenalizacionByIdAsync(int id);
        Task<ServiceResult<bool>> CreatePenalizacionAsync(PenalizacionAddDto penalizacionDto);
        Task<ServiceResult<bool>> LevantarPenalizacionAsync(int id);
        Task<ServiceResult<List<PenalizacionModel>>> GetPenalizacionesPorUsuarioAsync(int usuarioId);
    }
}
