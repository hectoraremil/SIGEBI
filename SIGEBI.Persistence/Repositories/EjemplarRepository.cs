using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class EjemplarRepository : IEjemplarRepository, IEjemplarDomainRepository
    {
        private readonly SigebiContext _context;

        public EjemplarRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<Ejemplar?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Ejemplares.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(Ejemplar entity, CancellationToken ct = default)
        {
            _context.Ejemplares.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Ejemplar entity, CancellationToken ct = default)
        {
            var ejemplar = await _context.Ejemplares.FindAsync(new object[] { entity.Id }, ct);

            if (ejemplar == null)
                throw new PersistenceException("El ejemplar que desea actualizar no existe.");

            ejemplar.CodigoInventario = entity.CodigoInventario;
            ejemplar.Estado = entity.Estado;
            ejemplar.Ubicacion = entity.Ubicacion;
            ejemplar.Activo = entity.Activo;
            ejemplar.RecursoBibliograficoId = entity.RecursoBibliograficoId;
            ejemplar.ModifyDate = entity.ModifyDate;
            ejemplar.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var ejemplar = await _context.Ejemplares.FindAsync(new object[] { id }, ct);

            if (ejemplar == null)
                throw new PersistenceException("El ejemplar que desea eliminar no existe.");

            ejemplar.Deleted = true;
            ejemplar.UserDeleted = userId;
            ejemplar.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region IEjemplarRepository

        public async Task<IReadOnlyList<Ejemplar>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Ejemplares
                .Where(e => !e.Deleted)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Ejemplar>> GetByRecursoIdAsync(int recursoId, CancellationToken ct = default)
        {
            return await _context.Ejemplares
                .Where(e => e.RecursoBibliograficoId == recursoId && !e.Deleted)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Ejemplar>> GetDisponiblesByRecursoIdAsync(int recursoId, CancellationToken ct = default)
        {
            return await _context.Ejemplares
                .Where(e => e.RecursoBibliograficoId == recursoId
                         && e.Estado == EstadoEjemplar.Disponible
                         && !e.Deleted)
                .ToListAsync(ct);
        }

        #endregion

        #region IEjemplarDomainRepository

        public async Task<bool> ExistsActiveAsync(int ejemplarId, CancellationToken ct = default)
        {
            return await _context.Ejemplares
                .AnyAsync(e => e.Id == ejemplarId && !e.Deleted, ct);
        }

        public async Task<bool> CodigoExistsAsync(string codigo, int? excludingId, CancellationToken ct = default)
        {
            return await _context.Ejemplares
                .AnyAsync(e => e.CodigoInventario == codigo && !e.Deleted && e.Id != excludingId, ct);
        }

        public async Task<bool> TienePrestamoActivoAsync(int ejemplarId, CancellationToken ct = default)
        {
            return await _context.Prestamos
                .AnyAsync(p => p.EjemplarId == ejemplarId
                            && p.Estado == EstadoPrestamo.Activo
                            && !p.Deleted, ct);
        }

        #endregion
    }
}
