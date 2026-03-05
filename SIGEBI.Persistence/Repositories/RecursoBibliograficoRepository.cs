using Microsoft.EntityFrameworkCore;
using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Persistence.Context;
using SIGEBI.Persistence.Exceptions;

namespace SIGEBI.Persistence.Repositories
{
    public sealed class RecursoBibliograficoRepository : IRecursoBibliograficoRepository, IRecursoBibliograficoDomainRepository
    {
        private readonly SigebiContext _context;

        public RecursoBibliograficoRepository(SigebiContext context)
        {
            _context = context;
        }

        #region IRepository

        public async Task<RecursoBibliografico?> GetByIdAsync(int id, CancellationToken ct = default)
        {
            return await _context.RecursosBibliograficos.FindAsync(new object[] { id }, ct);
        }

        public async Task AddAsync(RecursoBibliografico entity, CancellationToken ct = default)
        {
            _context.RecursosBibliograficos.Add(entity);
            await _context.SaveChangesAsync(ct);
        }

        public async Task UpdateAsync(RecursoBibliografico entity, CancellationToken ct = default)
        {
            var recurso = await _context.RecursosBibliograficos.FindAsync(new object[] { entity.Id }, ct);

            if (recurso == null)
                throw new PersistenceException("El recurso bibliográfico que desea actualizar no existe.");

            recurso.Titulo = entity.Titulo;
            recurso.Autor = entity.Autor;
            recurso.ISBN = entity.ISBN;
            recurso.Editorial = entity.Editorial;
            recurso.AnioPublicacion = entity.AnioPublicacion;
            recurso.Categoria = entity.Categoria;
            recurso.Descripcion = entity.Descripcion;
            recurso.Activo = entity.Activo;
            recurso.ModifyDate = entity.ModifyDate;
            recurso.UserMod = entity.UserMod;

            await _context.SaveChangesAsync(ct);
        }

        public async Task SoftDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            var recurso = await _context.RecursosBibliograficos.FindAsync(new object[] { id }, ct);

            if (recurso == null)
                throw new PersistenceException("El recurso bibliográfico que desea eliminar no existe.");

            recurso.Deleted = true;
            recurso.UserDeleted = userId;
            recurso.DeletedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync(ct);
        }

        #endregion

        #region IRecursoBibliograficoRepository

        public async Task<IReadOnlyList<RecursoBibliografico>> GetAllActiveAsync(CancellationToken ct = default)
        {
            return await _context.RecursosBibliograficos
                .Where(r => !r.Deleted && r.Activo)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<RecursoBibliografico>> GetByTituloAsync(string titulo, CancellationToken ct = default)
        {
            return await _context.RecursosBibliograficos
                .Where(r => r.Titulo.Contains(titulo) && !r.Deleted)
                .ToListAsync(ct);
        }

        public async Task<IReadOnlyList<RecursoBibliografico>> GetByCategoriaAsync(string categoria, CancellationToken ct = default)
        {
            return await _context.RecursosBibliograficos
                .Where(r => r.Categoria == categoria && !r.Deleted)
                .ToListAsync(ct);
        }

        #endregion

        #region IRecursoBibliograficoDomainRepository

        public async Task<bool> ExistsActiveAsync(int recursoId, CancellationToken ct = default)
        {
            return await _context.RecursosBibliograficos
                .AnyAsync(r => r.Id == recursoId && !r.Deleted, ct);
        }

        public async Task<bool> IsbnExistsAsync(string isbn, int? excludingId, CancellationToken ct = default)
        {
            return await _context.RecursosBibliograficos
                .AnyAsync(r => r.ISBN == isbn && !r.Deleted && r.Id != excludingId, ct);
        }

        #endregion
    }
}
