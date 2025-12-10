using FU_House_Finder_Auth.Repositories.Context;
using FU_House_Finder_Auth.Repositories.Interface;
using FU_House_Finder_Auth.Repositories.Models;
using Microsoft.EntityFrameworkCore;

namespace FU_House_Finder_Auth.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            user.Id = Guid.NewGuid();
            user.IsActive = true;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }
    }
}
