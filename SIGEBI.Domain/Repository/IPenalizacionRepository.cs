using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IPenalizacionRepository : IBaseRepository<Penalizacion>
    {
        Task<IEnumerable<Penalizacion>> GetActivasByUsuarioIdAsync(int usuarioId);
    }
}
