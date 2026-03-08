using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.RecursoBibliografico;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IRecursoBibliograficoService
    {
        Task<ServiceResult<List<RecursoBibliograficoModel>>> GetAllRecursosAsync();
        Task<ServiceResult<RecursoBibliograficoModel>> GetRecursoByIdAsync(int id);
        Task<ServiceResult<bool>> CreateRecursoAsync(RecursoBibliograficoAddDto recursoDto);
        Task<ServiceResult<bool>> UpdateRecursoAsync(int id, RecursoBibliograficoUpdateDto recursoDto);
        Task<ServiceResult<bool>> DeleteRecursoAsync(int id);
        
        Task<ServiceResult<List<RecursoBibliograficoModel>>> BuscarPorTituloAsync(string titulo);
        Task<ServiceResult<List<RecursoBibliograficoModel>>> BuscarPorCategoriaAsync(string categoria);
        Task<ServiceResult<List<RecursoBibliograficoModel>>> BuscarPorAutorAsync(string autor);
    }
}
