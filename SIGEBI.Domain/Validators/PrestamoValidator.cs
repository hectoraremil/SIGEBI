using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class PrestamoValidator : IPrestamoValidator
    {
        private readonly IPrestamoDomainRepository _prestamo;
        private readonly IUsuarioDomainRepository _usuario;
        private readonly IEjemplarDomainRepository _ejemplar;

        public PrestamoValidator(IPrestamoDomainRepository prestamoDomainRepository,
                                 IUsuarioDomainRepository usuarioDomainRepository,
                                 IEjemplarDomainRepository ejemplarDomainRepository)
        {
            _prestamo = prestamoDomainRepository;
            _usuario = usuarioDomainRepository;
            _ejemplar = ejemplarDomainRepository;
        }

        public async Task ValidateForCreateAsync(Prestamo entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.UsuarioId, 0, nameof(entity.UsuarioId));
            Guard.GreaterThan(entity.EjemplarId, 0, nameof(entity.EjemplarId));
            Guard.DateRange(entity.FechaPrestamo, entity.FechaVencimiento, "FechaVencimiento");

            if (!await _usuario.ExistsActiveAsync(entity.UsuarioId, ct))
                throw new DomainException("El usuario indicado no existe o está eliminado.");

            if (!await _ejemplar.ExistsActiveAsync(entity.EjemplarId, ct))
                throw new DomainException("El ejemplar indicado no existe o está eliminado.");

            if (!await _prestamo.EjemplarDisponibleAsync(entity.EjemplarId, ct))
                throw new DomainException("El ejemplar no está disponible para préstamo.");

            if (await _prestamo.UsuarioTienePrestamoActivoAsync(entity.UsuarioId, entity.EjemplarId, ct))
                throw new DomainException("El usuario ya tiene un préstamo activo para ese ejemplar.");
        }

        public async Task ValidateForUpdateAsync(Prestamo entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _prestamo.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("El préstamo no existe o está eliminado.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _prestamo.ExistsActiveAsync(id, ct))
                throw new DomainException("El préstamo no existe o ya está eliminado.");
        }
    }
}
