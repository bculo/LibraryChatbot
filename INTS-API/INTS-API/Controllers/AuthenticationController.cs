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
        public async Task<IActionResult> Login(AuthenticationRequestModel request)
        {
            var result = await _service.Login(request.UserName, request.Password);

            if (result.Success) //uspjesna prijava
            {
                var responseSuccess = new AuthenticationLoginReponseModel()
                {
                    Token = result.Token
                };

                return Ok(responseSuccess);
            }

            var responseError = new AuthenticationLoginReponseModel()
            {
                Errors = result.Errors,
                Success = false
            };

            return BadRequest(responseError);
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(AuthenticationRequestModel request)
        {
            var result = await _service.Register(request.UserName, request.Password);

            if (result.Success)
            {
                return Ok(new AuthenticationRegistrationResponseModel());
            }

            var responseError = new AuthenticationRegistrationResponseModel()
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
