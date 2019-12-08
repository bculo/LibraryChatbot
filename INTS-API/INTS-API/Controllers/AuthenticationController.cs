using INTS_API.Interfaces;
using INTS_API.Models.AuthenticationAPI;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace INTS_API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : LibraryMainController
    {
        private readonly IUserService _service;

        public AuthenticationController(IUserService service)
        {
            _service = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(AuthenticationRequest request)
        {
            var result = await _service.Login(request.UserName, request.Password);

            if (result.Success) //uspjesna prijava
            {
                var responseSuccess = new AuthenticationLoginReponse()
                {
                    Token = result.Token
                };

                return Ok(responseSuccess);
            }

            var responseError = new AuthenticationLoginReponse()
            {
                Errors = result.Errors,
                Success = false
            };

            return BadRequest(responseError);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticationRequest request)
        {
            var result = await _service.Register(request.UserName, request.Password);

            if (result.Success)
            {
                return Ok(new AuthenticationRegistrationResponse());
            }

            var responseError = new AuthenticationRegistrationResponse()
            {
                Errors = result.Errors,
                Success = false
            };

            return BadRequest(responseError);
        }

        public override string GetControllerName()
        {
            return nameof(AuthenticationController);
        }
    }
}
