using SIGEBI.Domain.Abstractions.Base;
using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IRecursoBibliograficoRepository : IRepository<RecursoBibliografico, int>
    {
        Task<IReadOnlyList<RecursoBibliografico>> GetAllActiveAsync(CancellationToken ct = default);
        Task<IReadOnlyList<RecursoBibliografico>> GetByTituloAsync(string titulo, CancellationToken ct = default);
        Task<IReadOnlyList<RecursoBibliografico>> GetByCategoriaAsync(string categoria, CancellationToken ct = default);
    }
}
