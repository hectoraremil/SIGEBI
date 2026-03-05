using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class DevolucionValidator : IDevolucionValidator
    {
        private readonly IDevolucionDomainRepository _devolucion;
        private readonly IPrestamoDomainRepository _prestamo;

        public DevolucionValidator(IDevolucionDomainRepository devolucionDomainRepository,
                                   IPrestamoDomainRepository prestamoDomainRepository)
        {
            _devolucion = devolucionDomainRepository;
            _prestamo = prestamoDomainRepository;
        }

        public async Task ValidateForCreateAsync(Devolucion entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.PrestamoId, 0, nameof(entity.PrestamoId));
            Guard.GreaterThan(entity.RegistradoPorUsuarioId, 0, nameof(entity.RegistradoPorUsuarioId));

            if (!await _prestamo.ExistsActiveAsync(entity.PrestamoId, ct))
                throw new DomainException("El préstamo indicado no existe o está eliminado.");

            if (await _devolucion.ExistsByPrestamoIdAsync(entity.PrestamoId, ct))
                throw new DomainException("El préstamo indicado ya tiene una devolución registrada.");
        }

        public async Task ValidateForUpdateAsync(Devolucion entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _devolucion.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("La devolución no existe o está eliminada.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _devolucion.ExistsActiveAsync(id, ct))
                throw new DomainException("La devolución no existe o ya está eliminada.");
        }
    }
}
