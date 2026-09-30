using Api.Common.Dtos.Roles;
using Api.Common.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController(IRoleService roleService) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleDto>>> GetRoles()
        {
            var roles = await roleService.GetRolesAsync();
            return Ok(roles);
        }
    }
}
