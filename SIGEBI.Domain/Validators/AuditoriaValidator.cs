using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class AuditoriaValidator : IAuditoriaValidator
    {
        private readonly IAuditoriaDomainRepository _auditoria;

        public AuditoriaValidator(IAuditoriaDomainRepository auditoriaDomainRepository)
        {
            _auditoria = auditoriaDomainRepository;
        }

        public Task ValidateForCreateAsync(Auditoria entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.Entidad, nameof(entity.Entidad), 100);
            Guard.NotNullOrWhiteSpace(entity.EntidadId, nameof(entity.EntidadId), 50);
            Guard.NotNullOrWhiteSpace(entity.Accion, nameof(entity.Accion), 100);
            Guard.GreaterThan(entity.UsuarioId, 0, nameof(entity.UsuarioId));

            return Task.CompletedTask;
        }

        public async Task ValidateForUpdateAsync(Auditoria entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));

            if (!await _auditoria.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("El registro de auditoría no existe o está eliminado.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _auditoria.ExistsActiveAsync(id, ct))
                throw new DomainException("El registro de auditoría no existe o ya está eliminado.");
        }
    }
}
