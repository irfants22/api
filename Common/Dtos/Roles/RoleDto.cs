using Api.Common.Dtos.Users;
using Api.Models;

namespace Api.Common.Dtos.Roles
{
    public class RoleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public IEnumerable<UserDto> Users { get; set; } = new List<UserDto>();
    }
}
