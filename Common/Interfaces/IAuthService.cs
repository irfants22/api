using Api.Common.Dtos.Users;

namespace Api.Common.Interfaces
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(SignInUserDto request);
        Task<UserDto?> RegisterAsync(SignUpUserDto request);
    }
}
