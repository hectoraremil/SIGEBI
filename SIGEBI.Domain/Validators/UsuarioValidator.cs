using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class UsuarioValidator : IUsuarioValidator
    {
        private readonly IUsuarioDomainRepository _usuario;
        private readonly IRolDomainRepository _rol;

        public UsuarioValidator(IUsuarioDomainRepository usuarioDomainRepository,
                                IRolDomainRepository rolDomainRepository)
        {
            _usuario = usuarioDomainRepository;
            _rol = rolDomainRepository;
        }

        public async Task ValidateForCreateAsync(Usuario entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.Nombre, nameof(entity.Nombre), 100);
            Guard.NotNullOrWhiteSpace(entity.Apellido, nameof(entity.Apellido), 100);
            Guard.NotNullOrWhiteSpace(entity.Email, nameof(entity.Email), 200);
            Guard.NotNullOrWhiteSpace(entity.PasswordHash, nameof(entity.PasswordHash));
            Guard.GreaterThan(entity.RolId, 0, nameof(entity.RolId));

            if (!await _rol.ExistsActiveAsync(entity.RolId, ct))
                throw new DomainException("El RolId indicado no existe o está eliminado.");

            if (await _usuario.EmailExistsAsync(entity.Email.Trim(), null, ct))
                throw new DomainException("Ya existe un usuario registrado con ese correo electrónico.");
        }

        public async Task ValidateForUpdateAsync(Usuario entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));
            Guard.NotNullOrWhiteSpace(entity.Nombre, nameof(entity.Nombre), 100);
            Guard.NotNullOrWhiteSpace(entity.Apellido, nameof(entity.Apellido), 100);
            Guard.NotNullOrWhiteSpace(entity.Email, nameof(entity.Email), 200);
            Guard.GreaterThan(entity.RolId, 0, nameof(entity.RolId));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _usuario.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("El usuario no existe o está eliminado.");

            if (!await _rol.ExistsActiveAsync(entity.RolId, ct))
                throw new DomainException("El RolId indicado no existe o está eliminado.");

            if (await _usuario.EmailExistsAsync(entity.Email.Trim(), entity.Id, ct))
                throw new DomainException("Ya existe otro usuario con ese correo electrónico.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _usuario.ExistsActiveAsync(id, ct))
                throw new DomainException("El usuario no existe o ya está eliminado.");
        }
    }
}
