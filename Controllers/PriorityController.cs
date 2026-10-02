using Api.Common.Dtos.Priority;
using Api.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/priorities")]
    [ApiController]
    public class PriorityController(IPriorityService priorityService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PriorityDto>>> GetPrioritiesAsync([FromQuery] PriorityQueryParamsDto queryParams)
        {
            var priorities = await priorityService.GetPrioritiesAsync(queryParams);
            return Ok(priorities);
        }

        [HttpGet("{id}", Name = "GetPriorityById")]
        public async Task<ActionResult<PriorityDto>> GetPriorityByIdAsync(int id)
        {
            var priority = await priorityService.GetPriorityByIdAsync(id);
            if (priority == null) return NotFound("Priority not found.");
            return Ok(priority);
        }

        [HttpPost]
        public async Task<ActionResult<PriorityDto>> CreatePriorityAsync([FromBody] CreatePriorityDto request)
        {
            var priority = await priorityService.CreatePriorityAsync(request);
            return CreatedAtRoute("GetPriorityById", new { id = priority?.Id }, priority);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdatePriorityAsync(int id, [FromBody] UpdatePriorityDto request)
        {
            var updatedPriority = await priorityService.UpdatePriorityAsync(id, request);
            if (!updatedPriority) return NotFound("Priority not found.");
            return Ok(updatedPriority);
        }
    }
}
