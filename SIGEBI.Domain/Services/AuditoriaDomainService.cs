using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services.Interfaces;

namespace SIGEBI.Domain.Services
{
    public class AuditoriaDomainService : IAuditoriaDomainService
    {
        private readonly IAuditoriaRepository _auditoriaRepository;

        public AuditoriaDomainService(IAuditoriaRepository auditoriaRepository)
        {
            _auditoriaRepository = auditoriaRepository;
        }

        public async Task<IEnumerable<Auditoria>> GetAllAsync()
        {
            return await _auditoriaRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Auditoria>> GetByEntidadAsync(string entidad, string entidadId)
        {
            return await _auditoriaRepository.GetByEntidadAsync(entidad, entidadId);
        }

        public async Task RegistrarAsync(Auditoria auditoria)
        {
            await _auditoriaRepository.AddAsync(auditoria);
        }
    }
}
