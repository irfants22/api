using Api.Common.Dtos.Users;
using Api.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        [HttpPost("login")]
        public async Task<ActionResult<string?>> LoginAsync(SignInUserDto request)
        {
            var token = await authService.LoginAsync(request);

            if (token is null) return BadRequest("Invalid email or password.");

            return Ok(token);
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserDto?>> RegisterAsync(SignUpUserDto request)
        {
            var newUser = await authService.RegisterAsync(request);

            if (newUser is null) return BadRequest("User already exists.");

            return Ok(newUser);
        }
    }
}
