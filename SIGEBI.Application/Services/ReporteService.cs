using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Enums;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;

namespace SIGEBI.Application.Services
{
    public sealed class ReporteService : IReporteService
    {
        private readonly IPrestamoRepository _prestamoRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IRecursoBibliograficoRepository _recursoRepository;
        private readonly IEjemplarRepository _ejemplarRepository;
        private readonly IPenalizacionRepository _penalizacionRepository;
        private readonly ILogger<ReporteService> _logger;

        public ReporteService(IPrestamoRepository prestamoRepository,
                              IUsuarioRepository usuarioRepository,
                              IRecursoBibliograficoRepository recursoRepository,
                              IEjemplarRepository ejemplarRepository,
                              IPenalizacionRepository penalizacionRepository,
                              ILogger<ReporteService> logger)
        {
            _prestamoRepository = prestamoRepository;
            _usuarioRepository = usuarioRepository;
            _recursoRepository = recursoRepository;
            _ejemplarRepository = ejemplarRepository;
            _penalizacionRepository = penalizacionRepository;
            _logger = logger;
        }

        public async Task<ServiceResult<List<PrestamoModel>>> GetReportePrestamosAsync()
        {
            ServiceResult<List<PrestamoModel>> serviceResult = new ServiceResult<List<PrestamoModel>>();

            try
            {
                var prestamos = await _prestamoRepository.GetAllAsync();

                serviceResult.Success = true;
                serviceResult.Message = "Reporte de prestamos generated successfully.";
                serviceResult.Data = prestamos.Select(p => new PrestamoModel
                {
                    Id = p.Id,
                    PrestamoId = p.Id,
                    UsuarioId = p.UsuarioId,
                    EjemplarId = p.EjemplarId,
                    FechaPrestamo = p.FechaPrestamo,
                    FechaVencimiento = p.FechaVencimiento,
                    Estado = p.Estado,
                    RenovacionesRealizadas = p.RenovacionesRealizadas,
                    CreadoPorUsuarioId = p.CreadoPorUsuarioId,
                    Activo = p.Activo
                }).ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while generating reporte de prestamos.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while generating reporte de prestamos.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<RecursoPopularModel>>> GetRecursosPopularesAsync()
        {
            ServiceResult<List<RecursoPopularModel>> serviceResult = new ServiceResult<List<RecursoPopularModel>>();

            try
            {
                var recursos = await _recursoRepository.GetAllAsync();
                var ejemplares = await _ejemplarRepository.GetAllAsync();
                var prestamos = await _prestamoRepository.GetAllAsync();

                var recursosPopulares = recursos.Select(r => new RecursoPopularModel
                {
                    RecursoBibliograficoId = r.Id,
                    Titulo = r.Titulo,
                    CantidadPrestamos = prestamos.Count(p => ejemplares.Any(e => e.Id == p.EjemplarId && e.RecursoBibliograficoId == r.Id))
                })
                .OrderByDescending(r => r.CantidadPrestamos)
                .ToList();

                serviceResult.Success = true;
                serviceResult.Message = "Recursos populares retrieved successfully.";
                serviceResult.Data = recursosPopulares;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting recursos populares.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting recursos populares.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<List<UsuarioMorosoModel>>> GetUsuariosMorososAsync()
        {
            ServiceResult<List<UsuarioMorosoModel>> serviceResult = new ServiceResult<List<UsuarioMorosoModel>>();

            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                var prestamosVencidos = await _prestamoRepository.GetVencidosAsync();

                var morosos = usuarios
                    .Where(u => prestamosVencidos.Any(p => p.UsuarioId == u.Id))
                    .Select(u => new UsuarioMorosoModel
                    {
                        UsuarioId = u.Id,
                        NombreCompleto = u.Nombre + " " + u.Apellido,
                        CantidadPrestamosVencidos = prestamosVencidos.Count(p => p.UsuarioId == u.Id)
                    })
                    .OrderByDescending(u => u.CantidadPrestamosVencidos)
                    .ToList();

                serviceResult.Success = true;
                serviceResult.Message = "Usuarios morosos retrieved successfully.";
                serviceResult.Data = morosos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting usuarios morosos.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting usuarios morosos.";
            }

            return serviceResult;
        }

        public async Task<ServiceResult<EstadisticaGeneralModel>> GetEstadisticasGeneralesAsync()
        {
            ServiceResult<EstadisticaGeneralModel> serviceResult = new ServiceResult<EstadisticaGeneralModel>();

            try
            {
                var usuarios = await _usuarioRepository.GetAllAsync();
                var recursos = await _recursoRepository.GetAllAsync();
                var ejemplares = await _ejemplarRepository.GetAllAsync();
                var prestamos = await _prestamoRepository.GetAllAsync();
                var penalizaciones = await _penalizacionRepository.GetAllAsync();

                serviceResult.Success = true;
                serviceResult.Message = "Estadisticas generales retrieved successfully.";
                serviceResult.Data = new EstadisticaGeneralModel
                {
                    TotalUsuarios = usuarios.Count,
                    TotalRecursos = recursos.Count,
                    TotalEjemplares = ejemplares.Count,
                    TotalPrestamosActivos = prestamos.Count(p => p.Estado == EstadoPrestamo.Activo),
                    TotalPenalizacionesActivas = penalizaciones.Count(p => p.Estado == EstadoPenalizacion.Activa)
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting estadisticas generales.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting estadisticas generales.";
            }

            return serviceResult;
        }
    }
}
