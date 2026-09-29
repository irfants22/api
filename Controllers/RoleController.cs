using Api.Common.Interfaces;
using Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Role>>> GetRoles()
        {
            var roles = await roleService.GetRolesAsync();
            return Ok(roles);
        }
    }
}
