using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Devolucion;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IDevolucionService
    {
        Task<ServiceResult<List<DevolucionModel>>> GetAllDevolucionesAsync();
        Task<ServiceResult<DevolucionModel>> GetDevolucionByIdAsync(int id);
        Task<ServiceResult<bool>> CreateDevolucionAsync(DevolucionAddDto devolucionDto);
    }
}
