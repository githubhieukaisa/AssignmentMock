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
    }
}
