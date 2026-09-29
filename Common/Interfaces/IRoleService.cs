using Api.Models;

namespace Api.Common.Interfaces
{
    public interface IRoleService
    {
        Task<List<Role>> GetRolesAsync();
    }
}
