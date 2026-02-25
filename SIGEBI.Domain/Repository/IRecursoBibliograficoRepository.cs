using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IRecursoBibliograficoRepository : IBaseRepository<RecursoBibliografico>
    {
        Task<IEnumerable<RecursoBibliografico>> GetByTituloAsync(string titulo);
        Task<IEnumerable<RecursoBibliografico>> GetByCategoriaAsync(string categoria);
    }
}
