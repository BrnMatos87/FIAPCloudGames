using Core.Input;
using Core.Service;
using Microsoft.AspNetCore.Mvc;

namespace FIAPCloudGames.Controllers
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

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginInput input)
        {
            var token = _authService.Login(input);

            return Ok(new
            {
                token
            });
        }
    }
}