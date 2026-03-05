using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class PenalizacionValidator : IPenalizacionValidator
    {
        private readonly IPenalizacionDomainRepository _penalizacion;
        private readonly IUsuarioDomainRepository _usuario;

        public PenalizacionValidator(IPenalizacionDomainRepository penalizacionDomainRepository,
                                     IUsuarioDomainRepository usuarioDomainRepository)
        {
            _penalizacion = penalizacionDomainRepository;
            _usuario = usuarioDomainRepository;
        }

        public async Task ValidateForCreateAsync(Penalizacion entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.UsuarioId, 0, nameof(entity.UsuarioId));
            Guard.NotNullOrWhiteSpace(entity.Motivo, nameof(entity.Motivo), 500);
            Guard.DateRange(entity.FechaInicio, entity.FechaFin, "FechaFin");

            if (!await _usuario.ExistsActiveAsync(entity.UsuarioId, ct))
                throw new DomainException("El usuario indicado no existe o está eliminado.");
        }

        public async Task ValidateForUpdateAsync(Penalizacion entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _penalizacion.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("La penalización no existe o está eliminada.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _penalizacion.ExistsActiveAsync(id, ct))
                throw new DomainException("La penalización no existe o ya está eliminada.");
        }
    }
}
