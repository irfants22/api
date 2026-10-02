using Api.Common.Dtos.Category;
using Api.Common.Dtos.Priority;

namespace Api.Common.Interfaces
{
    public interface IPriorityService
    {
        Task<IEnumerable<PriorityDto?>> GetPrioritiesAsync(PriorityQueryParamsDto queryParams);
        Task<PriorityDto?> GetPriorityByIdAsync(int id);
        Task<PriorityDto?> CreatePriorityAsync(CreatePriorityDto request);
        Task<bool> UpdatePriorityAsync(int id, UpdatePriorityDto request);

        //Task<bool> DeletePriorityAsync(int id);
    }
}
