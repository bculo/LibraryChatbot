using INTS_API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace INTS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : ControllerBase, IApiTest
    {
        private readonly IUserService _service;

        public AuthenticationController(IUserService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Test()
        {
            return Ok(nameof(AuthenticationController));
        }
    }
}
