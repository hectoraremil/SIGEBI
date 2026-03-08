using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class PrestamoRepository : IPrestamoRepository, IPrestamoDomainRepository
    {
        private readonly SigebiContext _context;

        public PrestamoRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<Prestamo?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Prestamos.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(Prestamo entity, CancellationToken ct = default)
        {
            _context.Prestamos.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Prestamo entity, CancellationToken ct = default)
        {
            var prestamo = await _context.Prestamos.FindAsync(new object[] { entity.Id }, ct);

            if (prestamo == null)
                throw new PersistenceException("El préstamo que desea actualizar no existe.");

            prestamo.Estado = entity.Estado;
            prestamo.Activo = entity.Activo;
            prestamo.FechaVencimiento = entity.FechaVencimiento;
            prestamo.RenovacionesRealizadas = entity.RenovacionesRealizadas;
            prestamo.ModifyDate = entity.ModifyDate;
            prestamo.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var prestamo = await _context.Prestamos.FindAsync(new object[] { id }, ct);

            if (prestamo == null)
                throw new PersistenceException("El préstamo que desea eliminar no existe.");

            prestamo.Deleted = true;
            prestamo.UserDeleted = userId;
            prestamo.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region IPrestamoRepository

        public async Task<IReadOnlyList<Prestamo>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Prestamos
                .Where(p => !p.Deleted)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Prestamo>> GetActivosByUsuarioIdAsync(int usuarioId, CancellationToken ct = default)
        {
            return await _context.Prestamos
                .Where(p => p.UsuarioId == usuarioId
                         && p.Estado == EstadoPrestamo.Activo
                         && !p.Deleted)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Prestamo>> GetVencidosAsync(CancellationToken ct = default)
        {
            return await _context.Prestamos
                .Where(p => p.Estado == EstadoPrestamo.Vencido && !p.Deleted)
                .ToListAsync(ct);
        }

        #endregion

        #region IPrestamoDomainRepository

        public async Task<bool> ExistsActiveAsync(int prestamoId, CancellationToken ct = default)
        {
            return await _context.Prestamos
                .AnyAsync(p => p.Id == prestamoId && !p.Deleted, ct);
        }

        public async Task<bool> EjemplarDisponibleAsync(int ejemplarId, CancellationToken ct = default)
        {
            return await _context.Ejemplares
                .AnyAsync(e => e.Id == ejemplarId
                            && e.Estado == EstadoEjemplar.Disponible
                            && !e.Deleted, ct);
        }

        public async Task<bool> UsuarioTienePrestamoActivoAsync(int usuarioId, int ejemplarId, CancellationToken ct = default)
        {
            return await _context.Prestamos
                .AnyAsync(p => p.UsuarioId == usuarioId
                            && p.EjemplarId == ejemplarId
                            && p.Estado == EstadoPrestamo.Activo
                            && !p.Deleted, ct);
        }

        #endregion
    }
}
