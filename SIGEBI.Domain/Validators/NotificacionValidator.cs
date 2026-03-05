using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class NotificacionValidator : INotificacionValidator
    {
        private readonly INotificacionDomainRepository _notificacion;
        private readonly IUsuarioDomainRepository _usuario;

        public NotificacionValidator(INotificacionDomainRepository notificacionDomainRepository,
                                     IUsuarioDomainRepository usuarioDomainRepository)
        {
            _notificacion = notificacionDomainRepository;
            _usuario = usuarioDomainRepository;
        }

        public async Task ValidateForCreateAsync(Notificacion entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.GreaterThan(entity.UsuarioId, 0, nameof(entity.UsuarioId));
            Guard.NotNullOrWhiteSpace(entity.Asunto, nameof(entity.Asunto), 200);
            Guard.NotNullOrWhiteSpace(entity.Contenido, nameof(entity.Contenido), 1000);

            if (!await _usuario.ExistsActiveAsync(entity.UsuarioId, ct))
                throw new DomainException("El usuario indicado no existe o está eliminado.");
        }

        public async Task ValidateForUpdateAsync(Notificacion entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _notificacion.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("La notificación no existe o está eliminada.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _notificacion.ExistsActiveAsync(id, ct))
                throw new DomainException("La notificación no existe o ya está eliminada.");
        }
    }
}
