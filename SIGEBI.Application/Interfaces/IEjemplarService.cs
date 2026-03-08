using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Ejemplar;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IEjemplarService
    {
        Task<ServiceResult<List<EjemplarModel>>> GetAllEjemplaresAsync();
        Task<ServiceResult<EjemplarModel>> GetEjemplarByIdAsync(int id);
        Task<ServiceResult<bool>> CreateEjemplarAsync(EjemplarAddDto ejemplarDto);
        Task<ServiceResult<bool>> UpdateEjemplarAsync(int id, EjemplarUpdateDto ejemplarDto);
        Task<ServiceResult<bool>> DeleteEjemplarAsync(int id);
    }
}
