using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario?> GetByEmailAsync(string email);
    }
}
