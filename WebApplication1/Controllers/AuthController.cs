using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.DTO;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        // POST: api/auth/register
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            var registerResult = await _authService.RegisterAsync(registerRequest);

            if (!registerResult.Succeeded)
            {
                return BadRequest(new { Message = "User registration failed." });
            }
            return Ok(new { Message = "User registration successful." });
        }

        // POST: api/auth/login
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var loginResult = await _authService.LoginAsync(loginRequest);

            if (!loginResult.Success)
            {
                return Unauthorized(new { Message = loginResult.ErrorMessage });
            }

            return Ok(new
            {
                loginResult.Token,
                Message = "Login successful."
            });
        }
    }
}