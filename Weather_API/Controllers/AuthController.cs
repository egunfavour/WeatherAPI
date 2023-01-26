using Microsoft.AspNetCore.Mvc;
using Weather_API.Core.DTO;
using Weather_API.Core.Interface;

namespace Weather_API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegistrationDTO model)
        {
            var result = await _authService.Register(model);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO model)
        {
            var response = await _authService.Login(model);
            return StatusCode(response.StatusCode, response);
        }

    }
}
