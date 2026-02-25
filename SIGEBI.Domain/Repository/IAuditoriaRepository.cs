using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IAuditoriaRepository : IBaseRepository<Auditoria>
    {
        Task<IEnumerable<Auditoria>> GetByEntidadAsync(string entidad, string entidadId);
    }
}
