using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface IDevolucionDomainService
    {
        Task<Devolucion?> GetByIdAsync(int id);
        Task<Devolucion?> GetByPrestamoIdAsync(int prestamoId);
        Task RegistrarAsync(Devolucion devolucion);
        Task UpdateAsync(Devolucion devolucion);
    }
}
