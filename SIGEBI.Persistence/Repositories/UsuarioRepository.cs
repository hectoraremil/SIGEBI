using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class UsuarioRepository : IUsuarioRepository, IUsuarioDomainRepository
    {
        private readonly SigebiContext _context;

        public UsuarioRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<Usuario?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.Usuarios.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(Usuario entity, CancellationToken ct = default)
        {
            _context.Usuarios.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(Usuario entity, CancellationToken ct = default)
        {
            var usuario = await _context.Usuarios.FindAsync(new object[] { entity.Id }, ct);

            if (usuario == null)
                throw new PersistenceException("El usuario que desea actualizar no existe.");

            usuario.Nombre = entity.Nombre;
            usuario.Apellido = entity.Apellido;
            usuario.Email = entity.Email;
            usuario.RolId = entity.RolId;
            usuario.Activo = entity.Activo;
            usuario.BloqueadoHasta = entity.BloqueadoHasta;
            usuario.ModifyDate = entity.ModifyDate;
            usuario.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var usuario = await _context.Usuarios.FindAsync(new object[] { id }, ct);

            if (usuario == null)
                throw new PersistenceException("El usuario que desea eliminar no existe.");

            usuario.Deleted = true;
            usuario.UserDeleted = userId;
            usuario.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region IUsuarioRepository

        public async Task<IReadOnlyList<Usuario>> GetAllAsync(CancellationToken ct = default)
        {
            return await _context.Usuarios
                .Where(u => !u.Deleted)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<Usuario>> GetAllActiveAsync(CancellationToken ct = default)
        {
            return await _context.Usuarios
                .Where(u => !u.Deleted)
                .ToListAsync(ct);
        }

        public async Task<Usuario?> GetByEmailAsync(string email, CancellationToken ct = default)
        {
            return await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == email && !u.Deleted, ct);
        }

        #endregion

        #region IUsuarioDomainRepository

        public async Task<bool> ExistsActiveAsync(int usuarioId, CancellationToken ct = default)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Id == usuarioId && !u.Deleted, ct);
        }

        public async Task<bool> EmailExistsAsync(string email, int? excludingId, CancellationToken ct = default)
        {
            return await _context.Usuarios
                .AnyAsync(u => u.Email == email && !u.Deleted && u.Id != excludingId, ct);
        }

        #endregion
    }
}
