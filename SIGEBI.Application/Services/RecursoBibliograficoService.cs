using Microsoft.Extensions.Logging;
using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.RecursoBibliografico;
using SIGEBI.Application.Interfaces;
using SIGEBI.Domain.Common;
using SIGEBI.Domain.Models;
using SIGEBI.Domain.Repository;
using SIGEBI.Domain.Validators.Interfaces;

namespace SIGEBI.Application.Services
{
    public sealed class RecursoBibliograficoService : IRecursoBibliograficoService
    {
        private readonly IRecursoBibliograficoRepository _recursoRepository;
        private readonly IRecursoBibliograficoValidator _recursoValidator;
        private readonly ILogger<RecursoBibliograficoService> _logger;

        public RecursoBibliograficoService(IRecursoBibliograficoRepository recursoRepository,
                                           IRecursoBibliograficoValidator recursoValidator,
                                           ILogger<RecursoBibliograficoService> logger)
        {
            _recursoRepository = recursoRepository;
            _recursoValidator = recursoValidator;
            _logger = logger;
        }

        public async Task<ServiceResult<List<RecursoBibliograficoModel>>> GetAllRecursosAsync()
        {
            ServiceResult<List<RecursoBibliograficoModel>> serviceResult = new ServiceResult<List<RecursoBibliograficoModel>>();
            
            try
            {
                _logger.LogInformation("Starting get all recursos process.");
                
                var recursos = await _recursoRepository.GetAllAsync();
                
                var recursosModel = recursos.Select(r => new RecursoBibliograficoModel
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Autor = r.Autor,
                    ISBN = r.ISBN,
                    Editorial = r.Editorial,
                    AnioPublicacion = r.AnioPublicacion,
                    Categoria = r.Categoria,
                    Descripcion = r.Descripcion,
                    Activo = r.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Recursos retrieved successfully.";
                serviceResult.Data = recursosModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all recursos.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting recursos.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<RecursoBibliograficoModel>> GetRecursoByIdAsync(int id)
        {
            ServiceResult<RecursoBibliograficoModel> serviceResult = new ServiceResult<RecursoBibliograficoModel>();
            
            try
            {
                _logger.LogInformation("Starting get recurso by id process. Id: {Id}", id);
                
                var recurso = await _recursoRepository.GetByIdAsync(id);
                
                if (recurso == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Recurso not found.";
                    return serviceResult;
                }
                
                RecursoBibliograficoModel recursoModel = new RecursoBibliograficoModel
                {
                    Id = recurso.Id,
                    Titulo = recurso.Titulo,
                    Autor = recurso.Autor,
                    ISBN = recurso.ISBN,
                    Editorial = recurso.Editorial,
                    AnioPublicacion = recurso.AnioPublicacion,
                    Categoria = recurso.Categoria,
                    Descripcion = recurso.Descripcion,
                    Activo = recurso.Activo
                };
                
                serviceResult.Success = true;
                serviceResult.Message = "Recurso retrieved successfully.";
                serviceResult.Data = recursoModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting recurso by id.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while getting recurso.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> CreateRecursoAsync(RecursoBibliograficoAddDto recursoDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting recurso creation process.");
            
            try
            {
                if (recursoDto == null)
                {
                    _logger.LogWarning("Recurso creation failed: RecursoBibliograficoAddDto is null.");
                    serviceResult.Success = false;
                    serviceResult.Message = "Recurso data is required.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                Domain.Entities.RecursoBibliografico recurso = new Domain.Entities.RecursoBibliografico
                {
                    Titulo = recursoDto.Titulo,
                    Autor = recursoDto.Autor,
                    ISBN = recursoDto.ISBN,
                    Editorial = recursoDto.Editorial,
                    AnioPublicacion = recursoDto.AnioPublicacion,
                    Categoria = recursoDto.Categoria,
                    Descripcion = recursoDto.Descripcion,
                    Activo = true
                };
                
                _logger.LogInformation("Recurso validation successful for: {@recurso}", recurso);
                
                await _recursoRepository.AddAsync(recurso);
                _logger.LogInformation("Recurso added to repository successfully: {@recurso}", recurso);
                
                serviceResult.Success = true;
                serviceResult.Message = "Recurso created successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while creating a recurso.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while creating a recurso.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while creating a recurso.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> UpdateRecursoAsync(int id, RecursoBibliograficoUpdateDto recursoDto)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting recurso update process. Id: {Id}", id);
            
            try
            {
                var recurso = await _recursoRepository.GetByIdAsync(id);
                
                if (recurso == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Recurso not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                recurso.Titulo = recursoDto.Titulo;
                recurso.Autor = recursoDto.Autor;
                recurso.ISBN = recursoDto.ISBN;
                recurso.Editorial = recursoDto.Editorial;
                recurso.AnioPublicacion = recursoDto.AnioPublicacion;
                recurso.Categoria = recursoDto.Categoria;
                recurso.Descripcion = recursoDto.Descripcion;
                recurso.Activo = recursoDto.Activo;
                
                await _recursoRepository.UpdateAsync(recurso);
                
                serviceResult.Success = true;
                serviceResult.Message = "Recurso updated successfully.";
                serviceResult.Data = true;
            }
            catch (DomainException dex)
            {
                _logger.LogWarning(dex, "Domain validation failed while updating a recurso.");
                serviceResult.Success = false;
                serviceResult.Message = dex.Message;
                serviceResult.Data = false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating a recurso.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while updating a recurso.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<bool>> DeleteRecursoAsync(int id)
        {
            ServiceResult<bool> serviceResult = new ServiceResult<bool>();
            
            _logger.LogInformation("Starting recurso deletion process. Id: {Id}", id);
            
            try
            {
                var recurso = await _recursoRepository.GetByIdAsync(id);
                
                if (recurso == null)
                {
                    serviceResult.Success = false;
                    serviceResult.Message = "Recurso not found.";
                    serviceResult.Data = false;
                    return serviceResult;
                }
                
                await _recursoRepository.SoftDeleteAsync(id, 0);
                
                serviceResult.Success = true;
                serviceResult.Message = "Recurso deleted successfully.";
                serviceResult.Data = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting a recurso.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while deleting a recurso.";
                serviceResult.Data = false;
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<List<RecursoBibliograficoModel>>> BuscarPorTituloAsync(string titulo)
        {
            ServiceResult<List<RecursoBibliograficoModel>> serviceResult = new ServiceResult<List<RecursoBibliograficoModel>>();
            
            try
            {
                _logger.LogInformation("Starting search recursos by titulo: {Titulo}", titulo);
                
                var recursos = await _recursoRepository.GetByTituloAsync(titulo);
                
                var recursosModel = recursos.Select(r => new RecursoBibliograficoModel
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Autor = r.Autor,
                    ISBN = r.ISBN,
                    Editorial = r.Editorial,
                    AnioPublicacion = r.AnioPublicacion,
                    Categoria = r.Categoria,
                    Descripcion = r.Descripcion,
                    Activo = r.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Recursos retrieved successfully.";
                serviceResult.Data = recursosModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching recursos by titulo.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while searching recursos.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<List<RecursoBibliograficoModel>>> BuscarPorCategoriaAsync(string categoria)
        {
            ServiceResult<List<RecursoBibliograficoModel>> serviceResult = new ServiceResult<List<RecursoBibliograficoModel>>();
            
            try
            {
                _logger.LogInformation("Starting search recursos by categoria: {Categoria}", categoria);
                
                var recursos = await _recursoRepository.GetByCategoriaAsync(categoria);
                
                var recursosModel = recursos.Select(r => new RecursoBibliograficoModel
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Autor = r.Autor,
                    ISBN = r.ISBN,
                    Editorial = r.Editorial,
                    AnioPublicacion = r.AnioPublicacion,
                    Categoria = r.Categoria,
                    Descripcion = r.Descripcion,
                    Activo = r.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Recursos retrieved successfully.";
                serviceResult.Data = recursosModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching recursos by categoria.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while searching recursos.";
            }
            
            return serviceResult;
        }

        public async Task<ServiceResult<List<RecursoBibliograficoModel>>> BuscarPorAutorAsync(string autor)
        {
            ServiceResult<List<RecursoBibliograficoModel>> serviceResult = new ServiceResult<List<RecursoBibliograficoModel>>();
            
            try
            {
                _logger.LogInformation("Starting search recursos by autor: {Autor}", autor);
                
                var todosRecursos = await _recursoRepository.GetAllAsync();
                var recursos = todosRecursos.Where(r => r.Autor.Contains(autor, StringComparison.OrdinalIgnoreCase));
                
                var recursosModel = recursos.Select(r => new RecursoBibliograficoModel
                {
                    Id = r.Id,
                    Titulo = r.Titulo,
                    Autor = r.Autor,
                    ISBN = r.ISBN,
                    Editorial = r.Editorial,
                    AnioPublicacion = r.AnioPublicacion,
                    Categoria = r.Categoria,
                    Descripcion = r.Descripcion,
                    Activo = r.Activo
                }).ToList();
                
                serviceResult.Success = true;
                serviceResult.Message = "Recursos retrieved successfully.";
                serviceResult.Data = recursosModel;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while searching recursos by autor.");
                serviceResult.Success = false;
                serviceResult.Message = "An error occurred while searching recursos.";
            }
            
            return serviceResult;
        }
    }
}
