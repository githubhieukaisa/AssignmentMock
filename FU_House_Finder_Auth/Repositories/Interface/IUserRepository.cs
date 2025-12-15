using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Repositories.Interface
{
    public interface IUserRepository
    {
        Task<User> RegisterUserAsync(User user);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string email);
        Task<User?> GetUserByIdAsync(int id);
        Task<User> UpdateUserAsync(User user);
        Task<RefreshToken> SaveRefreshTokenAsync(int userId, string token, DateTime expiryDate);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        Task<bool> RevokeRefreshTokenAsync(string token);
    }
}
