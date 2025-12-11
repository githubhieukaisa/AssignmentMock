using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Services
{
    public interface IJwtTokenService
    {
        string GenerateAccessToken(User user);
        string GenerateRefreshToken();
    }
}
