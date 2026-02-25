using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface IAuditoriaDomainService
    {
        Task<IEnumerable<Auditoria>> GetAllAsync();
        Task<IEnumerable<Auditoria>> GetByEntidadAsync(string entidad, string entidadId);
        Task RegistrarAsync(Auditoria auditoria);
    }
}
