using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class RolRepository : IRolRepository, IRolDomainRepository
    {
        private readonly SigebiContext _context;

        public RolRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<Rol?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Roles.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(Rol entity, CancellationToken ct = default)
        {
            _context.Roles.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Rol entity, CancellationToken ct = default)
        {
            var rol = await _context.Roles.FindAsync(new object[] { entity.Id }, ct);

            if (rol == null)
                throw new PersistenceException("El rol que desea actualizar no existe.");

            rol.Nombre = entity.Nombre;
            rol.ModifyDate = entity.ModifyDate;
            rol.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var rol = await _context.Roles.FindAsync(new object[] { id }, ct);

            if (rol == null)
                throw new PersistenceException("El rol que desea eliminar no existe.");

            rol.Deleted = true;
            rol.UserDeleted = userId;
            rol.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region IRolRepository

        public async Task<IReadOnlyList<Rol>> GetAllActiveAsync(CancellationToken ct = default)
        {
            return await _context.Roles
                .Where(r => !r.Deleted)
                .ToListAsync(ct);
        }

        #endregion

        #region IRolDomainRepository

        public async Task<bool> ExistsActiveAsync(int rolId, CancellationToken ct = default)
        {
            return await _context.Roles
                .AnyAsync(r => r.Id == rolId && !r.Deleted, ct);
        }

        public async Task<bool> NombreExistsAsync(string nombre, int? excludingId, CancellationToken ct = default)
        {
            return await _context.Roles
                .AnyAsync(r => r.Nombre == nombre && !r.Deleted && r.Id != excludingId, ct);
        }

        #endregion
    }
}
