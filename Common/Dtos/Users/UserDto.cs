using Api.Models;

namespace Api.Common.Dtos.Users
{
    public class UserDto
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
        public string Role { get; set; } = string.Empty;
    }
}
