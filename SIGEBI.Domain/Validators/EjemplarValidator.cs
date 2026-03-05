using SIGEBI.Domain.Abstractions;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Domain.Validators
{
    public sealed class EjemplarValidator : IEjemplarValidator
    {
        private readonly IEjemplarDomainRepository _ejemplar;
        private readonly IRecursoBibliograficoDomainRepository _recurso;

        public EjemplarValidator(IEjemplarDomainRepository ejemplarDomainRepository,
                                 IRecursoBibliograficoDomainRepository recursoDomainRepository)
        {
            _ejemplar = ejemplarDomainRepository;
            _recurso = recursoDomainRepository;
        }

        public async Task ValidateForCreateAsync(Ejemplar entity, CancellationToken ct = default)
        {
            Guard.NotNull(entity, nameof(entity));
            Guard.NotNullOrWhiteSpace(entity.CodigoInventario, nameof(entity.CodigoInventario), 50);
            Guard.GreaterThan(entity.RecursoBibliograficoId, 0, nameof(entity.RecursoBibliograficoId));

            if (!await _recurso.ExistsActiveAsync(entity.RecursoBibliograficoId, ct))
                throw new DomainException("El recurso bibliográfico indicado no existe o está eliminado.");

            if (await _ejemplar.CodigoExistsAsync(entity.CodigoInventario.Trim(), null, ct))
                throw new DomainException("Ya existe un ejemplar con ese código de inventario.");
        }

        public async Task ValidateForUpdateAsync(Ejemplar entity, CancellationToken ct = default)
        {
            Guard.GreaterThan(entity.Id, 0, nameof(entity.Id));
            Guard.NotNullOrWhiteSpace(entity.CodigoInventario, nameof(entity.CodigoInventario), 50);
            Guard.GreaterThan(entity.RecursoBibliograficoId, 0, nameof(entity.RecursoBibliograficoId));
            Guard.GreaterThan(entity.UserMod ?? 0, 0, nameof(entity.UserMod));

            if (!await _ejemplar.ExistsActiveAsync(entity.Id, ct))
                throw new DomainException("El ejemplar no existe o está eliminado.");

            if (!await _recurso.ExistsActiveAsync(entity.RecursoBibliograficoId, ct))
                throw new DomainException("El recurso bibliográfico indicado no existe o está eliminado.");

            if (await _ejemplar.CodigoExistsAsync(entity.CodigoInventario.Trim(), entity.Id, ct))
                throw new DomainException("Ya existe otro ejemplar con ese código de inventario.");
        }

        public async Task ValidateForDeleteAsync(int id, int userId, CancellationToken ct = default)
        {
            Guard.GreaterThan(id, 0, nameof(id));
            Guard.GreaterThan(userId, 0, nameof(userId));

            if (!await _ejemplar.ExistsActiveAsync(id, ct))
                throw new DomainException("El ejemplar no existe o ya está eliminado.");

            if (await _ejemplar.TienePrestamoActivoAsync(id, ct))
                throw new DomainException("No se puede eliminar el ejemplar porque tiene un préstamo activo.");
        }
    }
}
