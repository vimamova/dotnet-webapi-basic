using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Todo.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        public UsersController()
        {

        }
        [HttpGet]
        public async Task<IActionResult> GelHelloWorld()
        {
            return Ok("Hello World");
        }
    }
}
