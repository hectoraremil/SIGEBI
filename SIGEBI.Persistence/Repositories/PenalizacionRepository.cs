using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class PenalizacionRepository : IPenalizacionRepository, IPenalizacionDomainRepository
    {
        private readonly SigebiContext _context;

        public PenalizacionRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<Penalizacion?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Penalizaciones.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(Penalizacion entity, CancellationToken ct = default)
        {
            _context.Penalizaciones.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Penalizacion entity, CancellationToken ct = default)
        {
            var penalizacion = await _context.Penalizaciones.FindAsync(new object[] { entity.Id }, ct);

            if (penalizacion == null)
                throw new PersistenceException("La penalización que desea actualizar no existe.");

            penalizacion.Estado = entity.Estado;
            penalizacion.FechaFin = entity.FechaFin;
            penalizacion.Motivo = entity.Motivo;
            penalizacion.Activo = entity.Activo;
            penalizacion.ModifyDate = entity.ModifyDate;
            penalizacion.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var penalizacion = await _context.Penalizaciones.FindAsync(new object[] { id }, ct);

            if (penalizacion == null)
                throw new PersistenceException("La penalización que desea eliminar no existe.");

            penalizacion.Deleted = true;
            penalizacion.UserDeleted = userId;
            penalizacion.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region IPenalizacionRepository

        public async Task<IReadOnlyList<Penalizacion>> GetActivasByUsuarioIdAsync(int usuarioId, CancellationToken ct = default)
        {
            return await _context.Penalizaciones
                .Where(p => p.UsuarioId == usuarioId
                         && p.Estado == EstadoPenalizacion.Activa
                         && !p.Deleted)
                .ToListAsync(ct);
        }

        #endregion

        #region IPenalizacionDomainRepository

        public async Task<bool> ExistsActiveAsync(int penalizacionId, CancellationToken ct = default)
        {
            return await _context.Penalizaciones
                .AnyAsync(p => p.Id == penalizacionId && !p.Deleted, ct);
        }

        public async Task<bool> UsuarioTienePenalizacionActivaAsync(int usuarioId, CancellationToken ct = default)
        {
            return await _context.Penalizaciones
                .AnyAsync(p => p.UsuarioId == usuarioId
                            && p.Estado == EstadoPenalizacion.Activa
                            && !p.Deleted, ct);
        }

        #endregion
    }
}
