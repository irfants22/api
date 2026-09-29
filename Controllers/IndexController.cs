using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("/")]
    [ApiController]
    public class IndexController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var result = new
            {
                status = "OK",
                message = "Welcome to the API!",
                documentation = "http://localhost:5140/scalar"
            };
            return Ok(result);
        }
    }
}
