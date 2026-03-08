using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Usuario;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IUsuarioService
    {
        Task<ServiceResult<List<UsuarioModel>>> GetAllUsuariosAsync();
        Task<ServiceResult<UsuarioModel>> GetUsuarioByIdAsync(int id);
        Task<ServiceResult<bool>> CreateUsuarioAsync(UsuarioAddDto usuarioDto);
        Task<ServiceResult<bool>> UpdateUsuarioAsync(int id, UsuarioUpdateDto usuarioDto);
        Task<ServiceResult<bool>> DeleteUsuarioAsync(int id);
        
        Task<ServiceResult<LoginResultModel>> LoginAsync(LoginDto loginDto);
        Task<ServiceResult<bool>> CambiarPasswordAsync(CambiarPasswordDto cambiarPasswordDto);
    }
}
