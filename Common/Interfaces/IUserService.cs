using Api.Common.Dtos.Users;

namespace Api.Common.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto?>> GetUsersAsync(QueryParamsDto queryParams);
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<bool> UpdateUserAsync(int id, UpdateUserDto request);
        Task<bool> UpdateStatusUserAsync(int id, bool isActive);
        Task<bool> DeleteUserAsync(int id);
    }
}
