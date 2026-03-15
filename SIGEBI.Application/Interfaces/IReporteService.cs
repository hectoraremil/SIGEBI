using SIGEBI.Application.Base;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IReporteService
    {
        Task<ServiceResult<List<PrestamoModel>>> GetReportePrestamosAsync();
        Task<ServiceResult<List<RecursoPopularModel>>> GetRecursosPopularesAsync();
        Task<ServiceResult<List<UsuarioMorosoModel>>> GetUsuariosMorososAsync();
        Task<ServiceResult<EstadisticaGeneralModel>> GetEstadisticasGeneralesAsync();
    }
}
