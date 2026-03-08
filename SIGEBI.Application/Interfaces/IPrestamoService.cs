using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Prestamo;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IPrestamoService
    {
        Task<ServiceResult<List<PrestamoModel>>> GetAllPrestamosAsync();
        Task<ServiceResult<PrestamoModel>> GetPrestamoByIdAsync(int id);
        Task<ServiceResult<bool>> CreatePrestamoAsync(PrestamoAddDto prestamoDto);
        Task<ServiceResult<bool>> UpdatePrestamoAsync(int id, PrestamoUpdateDto prestamoDto);
        Task<ServiceResult<bool>> DeletePrestamoAsync(int id);
        
        Task<ServiceResult<bool>> RenovarPrestamoAsync(int id, int diasExtension);
        Task<ServiceResult<List<PrestamoModel>>> GetPrestamosActivosPorUsuarioAsync(int usuarioId);
        Task<ServiceResult<List<PrestamoModel>>> GetPrestamosVencidosAsync();
    }
}
