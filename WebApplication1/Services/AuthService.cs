using Microsoft.AspNetCore.Identity;
using WebApplication1.Models.DTO;

namespace WebApplication1.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<IdentityUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public async Task<IdentityResult> RegisterAsync(RegisterRequestDTO registerRequest)
        {
            var user = new IdentityUser
            {
                UserName = registerRequest.Username,
                Email = registerRequest.Email
            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "User");
            }
            return result;
        }

        public async Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
                return new LoginResultDTO
                {
                    Success = false,
                    Token = "",
                    ErrorMessage = "Invalid email or password."
                };

            var passwordCheck = await _userManager.CheckPasswordAsync(user, loginRequest.Password);
            if (!passwordCheck)
                return new LoginResultDTO
                {
                    Success = false,
                    Token = "",
                };

            var roles = await _userManager.GetRolesAsync(user);
            var token = _tokenService.CreateJWTToken(user, roles);

            return new LoginResultDTO
            {
                Success = true,
                Token = token,
                ErrorMessage = null
            };
        }
    }
}