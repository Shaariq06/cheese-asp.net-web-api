using Microsoft.AspNetCore.Identity;

namespace WebApplication1.Services
{
    public interface ITokenService
    {
        string CreateJWTToken(IdentityUser user, IList<string> roles);
    }
}