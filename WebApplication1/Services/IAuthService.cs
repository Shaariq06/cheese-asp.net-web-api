using WebApplication1.Models.DTO;
using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Services
{
    public interface IAuthService
    {
        Task<IdentityResult> RegisterAsync(RegisterRequestDTO registerRequest);
        Task<LoginResultDTO> LoginAsync(LoginRequestDTO loginRequest);
    }
}