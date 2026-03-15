using SIGEBI.Application.Base;
using SIGEBI.Application.Dtos.Rol;
using SIGEBI.Domain.Models;

namespace SIGEBI.Application.Interfaces
{
    public interface IRolService
    {
        Task<ServiceResult<List<RolModel>>> GetRolesAsync();
        Task<ServiceResult<RolModel>> GetRolByIdAsync(int id);
        Task<ServiceResult<bool>> CreateRolAsync(RolAddDto rolDto);
        Task<ServiceResult<bool>> UpdateRolAsync(int id, RolUpdateDto rolDto);
        Task<ServiceResult<bool>> DeleteRolAsync(int id);
    }
}
