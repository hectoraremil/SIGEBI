using SIGEBI.Application.Base;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IAuditoriaService
    {
        Task<ServiceResult<List<AuditoriaModel>>> GetAllAuditoriasAsync();
        Task<ServiceResult<AuditoriaModel>> GetAuditoriaByIdAsync(int id);
        Task<ServiceResult<List<AuditoriaModel>>> GetAuditoriasPorEntidadAsync(string entidad, int entidadId);
    }
}
