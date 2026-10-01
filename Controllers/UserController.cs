using Api.Common.Dtos.Users;
using Api.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public record UpdateStatusUserRequest(bool IsActive);

    [Route("api/users")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsersAsync([FromQuery] QueryParamsDto queryParams)
        {
            var users = await userService.GetUsersAsync(queryParams);
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserByIdAsync(int id )
        {
            var user = await userService.GetUserByIdAsync(id);
            if (user == null) return NotFound("User not found.");
            return Ok(user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateUserAsync(int id, [FromBody] UpdateUserDto request)
        {
            var updatedUser = await userService.UpdateUserAsync(id, request);
            if (!updatedUser) return NotFound("User not found.");
            return Ok(updatedUser);
        }

        [HttpPatch("{id}/status")]
        public async Task<ActionResult<bool>> UpdateStatusUserAsync(int id, [FromBody] UpdateStatusUserRequest request)
        {
            var updatedUser = await userService.UpdateStatusUserAsync(id, request.IsActive);
            if (!updatedUser) return NotFound("User not found.");
            return Ok(updatedUser);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteUserAsync(int id)
        {
            var deletedUser = await userService.DeleteUserAsync(id);
            if (!deletedUser) return NotFound("User not found.");
            return Ok(deletedUser);
        }
    }
}
