using Microsoft.EntityFrameworkCore;
using Api.Common.Interfaces;
using Api.Data;
using Api.Common.Dtos.Roles;
using Api.Common.Dtos.Users;

namespace Api.Services
{
    public class RoleService(ApplicationDbContext context) : IRoleService
    {
        public async Task<IEnumerable<RoleDto>> GetRolesAsync()
        {
            var roles = await context.Roles
                .Include(r => r.Users)
                .Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Users = r.Users.Select(u => new UserDto
                    {
                        Name = u.Name,
                        Email = u.Email,
                        IsActive = u.IsActive,
                        Role = r.Name
                    })
                })
                .ToListAsync();

            return roles;
        }
    }
}
