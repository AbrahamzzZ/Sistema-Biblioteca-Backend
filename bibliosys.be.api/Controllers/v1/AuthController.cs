using System.Threading.Tasks;
using bibliosys.be.application.Interfaces.Service;
using Microsoft.AspNetCore.Mvc;

namespace bibliosys.be.api.Controllers.v1
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

        public class LoginRequest
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.LoginAsync(request.Email, request.Password);
            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(new { token });
        }
    }
}

