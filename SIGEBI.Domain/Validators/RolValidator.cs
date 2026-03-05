using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class RolValidator : IRolValidator
    {
        private readonly IRolDomainRepository _rol;

        public RolValidator(IRolDomainRepository rolDomainRepository)
        {
            _rol = rolDomainRepository;
        }

        public async Task ValidateForCreateAsync(Rol entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.Nombre, nameof(entity.Nombre), 100);

            if (await _rol.NombreExistsAsync(entity.Nombre.Trim(), null, ct))
                throw new DomainException("Ya existe un rol con ese nombre.");
        }

        public async Task ValidateForUpdateAsync(Rol entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));
            Guard.NotNullOrWhiteSpace(entity.Nombre, nameof(entity.Nombre), 100);
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _rol.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("El rol no existe o está eliminado.");

            if (await _rol.NombreExistsAsync(entity.Nombre.Trim(), entity.Id, ct))
                throw new DomainException("Ya existe otro rol con ese nombre.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _rol.ExistsActiveAsync(id, ct))
                throw new DomainException("El rol no existe o ya está eliminado.");
        }
    }
}
