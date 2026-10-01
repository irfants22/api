using Api.Common.Dtos.Roles;

namespace Api.Common.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleDto?>> GetRolesAsync();
    }
}
