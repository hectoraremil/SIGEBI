using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class RecursoBibliograficoValidator : IRecursoBibliograficoValidator
    {
        private readonly IRecursoBibliograficoDomainRepository _recurso;

        public RecursoBibliograficoValidator(IRecursoBibliograficoDomainRepository recursoDomainRepository)
        {
            _recurso = recursoDomainRepository;
        }

        public async Task ValidateForCreateAsync(RecursoBibliografico entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.Titulo, nameof(entity.Titulo), 300);
            Guard.NotNullOrWhiteSpace(entity.Autor, nameof(entity.Autor), 200);
            Guard.NotNullOrWhiteSpace(entity.ISBN, nameof(entity.ISBN), 20);

            if (await _recurso.IsbnExistsAsync(entity.ISBN!.Trim(), null, ct))
                throw new DomainException("Ya existe un recurso bibliográfico con ese ISBN.");
        }

        public async Task ValidateForUpdateAsync(RecursoBibliografico entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));
            Guard.NotNullOrWhiteSpace(entity.Titulo, nameof(entity.Titulo), 300);
            Guard.NotNullOrWhiteSpace(entity.Autor, nameof(entity.Autor), 200);
            Guard.NotNullOrWhiteSpace(entity.ISBN, nameof(entity.ISBN), 20);
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _recurso.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("El recurso bibliográfico no existe o está eliminado.");

            if (await _recurso.IsbnExistsAsync(entity.ISBN.Trim(), entity.Id, ct))
                throw new DomainException("Ya existe otro recurso bibliográfico con ese ISBN.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _recurso.ExistsActiveAsync(id, ct))
                throw new DomainException("El recurso bibliográfico no existe o ya está eliminado.");
        }
    }
}
