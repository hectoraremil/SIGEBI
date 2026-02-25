using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IPrestamoRepository : IBaseRepository<Prestamo>
    {
        Task<IEnumerable<Prestamo>> GetActivosByUsuarioIdAsync(int usuarioId);
        Task<IEnumerable<Prestamo>> GetVencidosAsync();
    }
}
