using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface IRecursoBibliograficoDomainService
    {
        Task<RecursoBibliografico?> GetByIdAsync(int id);
        Task<IEnumerable<RecursoBibliografico>> GetAllAsync();
        Task AddAsync(RecursoBibliografico recurso);
        Task UpdateAsync(RecursoBibliografico recurso);
        Task DeleteAsync(int id);
        Task<IEnumerable<RecursoBibliografico>> BuscarAsync(string termino);
    }
}
