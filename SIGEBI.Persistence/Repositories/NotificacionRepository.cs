using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class NotificacionRepository : INotificacionRepository, INotificacionDomainRepository
    {
        private readonly SigebiContext _context;

        public NotificacionRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<Notificacion?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Notificaciones.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(Notificacion entity, CancellationToken ct = default)
        {
            _context.Notificaciones.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Notificacion entity, CancellationToken ct = default)
        {
            var notificacion = await _context.Notificaciones.FindAsync(new object[] { entity.Id }, ct);

            if (notificacion == null)
                throw new PersistenceException("La notificación que desea actualizar no existe.");

            notificacion.Estado = entity.Estado;
            notificacion.FechaEnvio = entity.FechaEnvio;
            notificacion.Activo = entity.Activo;
            notificacion.ModifyDate = entity.ModifyDate;
            notificacion.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var notificacion = await _context.Notificaciones.FindAsync(new object[] { id }, ct);

            if (notificacion == null)
                throw new PersistenceException("La notificación que desea eliminar no existe.");

            notificacion.Deleted = true;
            notificacion.UserDeleted = userId;
            notificacion.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region INotificacionRepository

        public async Task<IReadOnlyList<Notificacion>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Notificaciones
                .Where(n => !n.Deleted)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Notificacion>> GetByUsuarioIdAsync(int usuarioId, CancellationToken ct = default)
        {
            return await _context.Notificaciones
                .Where(n => n.UsuarioId == usuarioId && !n.Deleted)
                .ToListAsync(ct);
        }

        #endregion

        #region INotificacionDomainRepository

        public async Task<bool> ExistsActiveAsync(int notificacionId, CancellationToken ct = default)
        {
            return await _context.Notificaciones
                .AnyAsync(n => n.Id == notificacionId && !n.Deleted, ct);
        }

        #endregion
    }
}
