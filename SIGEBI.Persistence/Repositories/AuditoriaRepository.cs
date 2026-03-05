using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class AuditoriaRepository : IAuditoriaRepository, IAuditoriaDomainRepository
    {
        private readonly SigebiContext _context;

        public AuditoriaRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<Auditoria?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Auditorias.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(Auditoria entity, CancellationToken ct = default)
        {
            _context.Auditorias.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Auditoria entity, CancellationToken ct = default)
        {
            var auditoria = await _context.Auditorias.FindAsync(new object[] { entity.Id }, ct);

            if (auditoria == null)
                throw new PersistenceException("El registro de auditoría que desea actualizar no existe.");

            auditoria.Activo = entity.Activo;
            auditoria.ModifyDate = entity.ModifyDate;
            auditoria.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var auditoria = await _context.Auditorias.FindAsync(new object[] { id }, ct);

            if (auditoria == null)
                throw new PersistenceException("El registro de auditoría que desea eliminar no existe.");

            auditoria.Deleted = true;
            auditoria.UserDeleted = userId;
            auditoria.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region IAuditoriaRepository

        public async Task<IReadOnlyList<Auditoria>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Auditorias
                .Where(a => !a.Deleted)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Auditoria>> GetByEntidadAsync(string entidad, string entidadId, CancellationToken ct = default)
        {
            return await _context.Auditorias
                .Where(a => a.Entidad == entidad && a.EntidadId == entidadId && !a.Deleted)
                .ToListAsync(ct);
        }

        #endregion

        #region IAuditoriaDomainRepository

        public async Task<bool> ExistsActiveAsync(int auditoriaId, CancellationToken ct = default)
        {
            return await _context.Auditorias
                .AnyAsync(a => a.Id == auditoriaId && !a.Deleted, ct);
        }

        #endregion
    }
}
