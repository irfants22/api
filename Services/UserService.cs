using Api.Common.Dtos.Users;
using Api.Common.Interfaces;
using Api.Data;
using Api.Models;
using Azure.Core;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class UserService(ApplicationDbContext context) : IUserService
    {
        public async Task<IEnumerable<UserDto?>> GetUsersAsync(UserQueryParamsDto queryParams)
        {
            IQueryable<User> users = context.Users;

            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                users = users.Where(u => u.Name.Contains(queryParams.SearchTerm) || u.Email.Contains(queryParams.SearchTerm));
            }

            var skip = (queryParams.PageNumber - 1) * queryParams.PageSize;

            var result = await users
                .Include(u => u.Role)
                .Skip(skip)
                .Take(queryParams.PageSize)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    Role = u.Role!.Name
                })
                .ToListAsync();

            return result;
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            if (!await context.Users.AnyAsync(u => u.Id == id))
            {
                return null;
            }

            var user = await context.Users
                .Include(u => u.Role)
                .Where(u => u.Id == id)
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    IsActive = u.IsActive,
                    Role = u.Role!.Name
                })
                .FirstOrDefaultAsync();

            return user;
        }

        public async Task<bool> UpdateUserAsync(int id, UpdateUserDto request)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null) return false;

            user.Name = request.Name ?? user.Name;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> UpdateStatusUserAsync(int id, bool isActive)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null) return false;

            user.IsActive = isActive;

            await context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await context.Users.FindAsync(id);

            if (user == null) return false;

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return true;
        }
    }
}
