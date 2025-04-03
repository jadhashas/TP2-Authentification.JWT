using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentification.JWT.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecureController : ControllerBase
    {
        [HttpGet("hello")]
        [Authorize]
        public IActionResult SayHello()
        {
            return Ok("Hello from a protected endpoint!");
        }
    }
}
