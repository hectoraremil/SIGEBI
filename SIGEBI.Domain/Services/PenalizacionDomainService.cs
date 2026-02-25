using SIGEBI.Domain.Entities;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Services.Interfaces;

namespace SIGEBI.Domain.Services
{
    public class PenalizacionDomainService : IPenalizacionDomainService
    {
        private readonly IPenalizacionRepository _penalizacionRepository;

        public PenalizacionDomainService(IPenalizacionRepository penalizacionRepository)
        {
            _penalizacionRepository = penalizacionRepository;
        }

        public async Task<Penalizacion?> GetByIdAsync(int id)
        {
            return await _penalizacionRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Penalizacion>> GetByUsuarioIdAsync(int usuarioId)
        {
            return await _penalizacionRepository.GetActivasByUsuarioIdAsync(usuarioId);
        }

        public async Task<bool> TieneActivaAsync(int usuarioId)
        {
            var penalizaciones = await _penalizacionRepository.GetActivasByUsuarioIdAsync(usuarioId);
            return penalizaciones.Any();
        }

        public async Task AddAsync(Penalizacion penalizacion)
        {
            await _penalizacionRepository.AddAsync(penalizacion);
        }

        public async Task LevantarAsync(int penalizacionId)
        {
            var penalizacion = await _penalizacionRepository.GetByIdAsync(penalizacionId);
            if (penalizacion != null)
            {
                penalizacion.Estado = EstadoPenalizacion.Cancelada;
                await _penalizacionRepository.UpdateAsync(penalizacion);
            }
        }
    }
}
