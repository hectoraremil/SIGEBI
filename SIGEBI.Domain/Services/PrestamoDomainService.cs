using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services.Interfaces;

namespace SIGEBI.Domain.Services
{
    public class PrestamoDomainService : IPrestamoDomainService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IPenalizacionRepository _penalizacionRepository;

        public PrestamoDomainService(IPrestamoRepository prestamoRepository, IPenalizacionRepository penalizacionRepository)
        {
            _prestamoRepository = prestamoRepository;
            _penalizacionRepository = penalizacionRepository;
        }

        public async Task<Prestamo?> GetByIdAsync(int id)
        {
            return await _prestamoRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Prestamo>> GetAllAsync()
        {
            return await _prestamoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Prestamo>> GetActivosByUsuarioIdAsync(int usuarioId)
        {
            return await _prestamoRepository.GetActivosByUsuarioIdAsync(usuarioId);
        }

        public async Task RegistrarAsync(Prestamo prestamo)
        {
            await _prestamoRepository.AddAsync(prestamo);
        }

        public async Task UpdateAsync(Prestamo prestamo)
        {
            await _prestamoRepository.UpdateAsync(prestamo);
        }

        public async Task<bool> UsuarioPuedePrestarAsync(int usuarioId)
        {
            var penalizaciones = await _penalizacionRepository.GetActivasByUsuarioIdAsync(usuarioId);
            return !penalizaciones.Any();
        }
    }
}
