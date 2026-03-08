using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class DevolucionRepository : IDevolucionRepository, IDevolucionDomainRepository
    {
        private readonly SigebiContext _context;

        public DevolucionRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<Devolucion?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Devoluciones.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(Devolucion entity, CancellationToken ct = default)
        {
            _context.Devoluciones.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Devolucion entity, CancellationToken ct = default)
        {
            var devolucion = await _context.Devoluciones.FindAsync(new object[] { entity.Id }, ct);

            if (devolucion == null)
                throw new PersistenceException("La devolución que desea actualizar no existe.");

            devolucion.Observaciones = entity.Observaciones;
            devolucion.DiasAtraso = entity.DiasAtraso;
            devolucion.ModifyDate = entity.ModifyDate;
            devolucion.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var devolucion = await _context.Devoluciones.FindAsync(new object[] { id }, ct);

            if (devolucion == null)
                throw new PersistenceException("La devolución que desea eliminar no existe.");

            devolucion.Deleted = true;
            devolucion.UserDeleted = userId;
            devolucion.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region IDevolucionRepository

        public async Task<IReadOnlyList<Devolucion>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Devoluciones
                .Where(d => !d.Deleted)
                .ToListAsync(ct);
        }

        public async Task<Devolucion?> GetByPrestamoIdAsync(int prestamoId, CancellationToken ct = default)
        {
            return await _context.Devoluciones
                .FirstOrDefaultAsync(d => d.PrestamoId == prestamoId && !d.Deleted, ct);
        }

        #endregion

        #region IDevolucionDomainRepository

        public async Task<bool> ExistsActiveAsync(int devolucionId, CancellationToken ct = default)
        {
            return await _context.Devoluciones
                .AnyAsync(d => d.Id == devolucionId && !d.Deleted, ct);
        }

        public async Task<bool> ExistsByPrestamoIdAsync(int prestamoId, CancellationToken ct = default)
        {
            return await _context.Devoluciones
                .AnyAsync(d => d.PrestamoId == prestamoId && !d.Deleted, ct);
        }

        #endregion
    }
}
