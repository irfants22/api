using Api.Common.Dtos.Priority;
using Api.Common.Interfaces;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class PriorityService(ApplicationDbContext context) : IPriorityService
    {
        public async Task<IEnumerable<PriorityDto?>> GetPrioritiesAsync(PriorityQueryParamsDto queryParams)
        {
            IQueryable<Priority> priorities = context.Priorities;

            if (!string.IsNullOrEmpty(queryParams.SearchTerm))
            {
                priorities = priorities.Where(p => p.Name.Contains(queryParams.SearchTerm));
            }

            var skip = (queryParams.PageNumber - 1) * queryParams.PageSize;

            var result = await priorities
                .Skip(skip)
                .Take(queryParams.PageSize)
                .Select(p => new PriorityDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Level = p.Level,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .ToListAsync();

            return result;
        }

        public async Task<PriorityDto?> GetPriorityByIdAsync(int id)
        {
            if (!await context.Priorities.AnyAsync(p => p.Id == id))
            {
                return null;
            }

            var priority = await context.Priorities
                .Where(p => p.Id == id)
                .Select(p => new PriorityDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Level = p.Level,
                    CreatedAt = p.CreatedAt,
                    UpdatedAt = p.UpdatedAt
                })
                .FirstOrDefaultAsync();

            return priority;
        }

        public async Task<PriorityDto?> CreatePriorityAsync(CreatePriorityDto request)
        {
            var Priority = new Priority
            {
                Name = request.Name,
                Level = request.Level,
            };

            context.Priorities.Add(Priority);
            await context.SaveChangesAsync();

            return new PriorityDto
            {
                Id = Priority.Id,
                Name = Priority.Name,
                Level = Priority.Level,
                CreatedAt = Priority.CreatedAt,
                UpdatedAt = Priority.UpdatedAt
            };
        }

        public async Task<bool> UpdatePriorityAsync(int id, UpdatePriorityDto request)
        {
            var priority = await context.Priorities.FindAsync(id);

            if (priority == null) return false;

            priority.Name = request.Name ?? priority.Name;
            priority.Level = request.Level ?? priority.Level;

            await context.SaveChangesAsync();

            return true;
        }
    }
}
