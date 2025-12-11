using FU_House_Finder_Auth.Dtos;
using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Services
{
    public interface IUserService
    {
        Task<User> RegisterAsync(RegisterDto registerDto, UserRole role);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<LoginResponseDto> RefreshTokenAsync(string refreshToken);
        Task<UserProfileDto> GetUserProfileAsync(int userId);
        Task<UserProfileDto> UpdateUserProfileAsync(int userId, ChangeProfileDto changeProfileDto);
    }
}
