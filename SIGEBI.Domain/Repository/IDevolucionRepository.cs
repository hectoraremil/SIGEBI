using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Repository
{
    public interface IDevolucionRepository : IBaseRepository<Devolucion>
    {
        Task<Devolucion?> GetByPrestamoIdAsync(int prestamoId);
    }
}
