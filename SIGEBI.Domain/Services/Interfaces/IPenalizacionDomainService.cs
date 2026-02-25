using SIGEBI.Domain.Entities;

namespace SIGEBI.Domain.Services.Interfaces
{
    public interface IPenalizacionDomainService
    {
        Task<Penalizacion?> GetByIdAsync(int id);
        Task<IEnumerable<Penalizacion>> GetByUsuarioIdAsync(int usuarioId);
        Task<bool> TieneActivaAsync(int usuarioId);
        Task AddAsync(Penalizacion penalizacion);
        Task LevantarAsync(int penalizacionId);
    }
}
