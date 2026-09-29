using Microsoft.EntityFrameworkCore;
using Api.Common.Interfaces;
using Api.Data;
using Api.Models;

namespace Api.Services
{
    public class RoleService(ApplicationDbContext context) : IRoleService
    {
        public async Task<List<Role>> GetRolesAsync()
        {
            var roles = await context.Roles
                .Include(r => r.Users)
                .Select(r => new Role
                {
                    Id = r.Id,
                    Name = r.Name,
                    Description = r.Description,
                    Users = r.Users,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                })
                .ToListAsync();
            return roles;
        }
    }
}
