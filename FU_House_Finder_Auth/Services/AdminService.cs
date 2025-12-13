
using Microsoft.EntityFrameworkCore;
using FU_House_Finder_Auth.Dtos;
using FU_House_Finder_Auth.Repositories.Context;
using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Services
{
    public class AdminService : IAdminService
    {
        private readonly AppDbContext _context;

        public AdminService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<UserPublicDto>> GetPendingLandlordsAsync()
        {
            return await _context.Users
                .Where(u => u.Role == UserRole.Landlord && !u.IsActive)
                .Select(u => new UserPublicDto
                {
                    Id = u.Id,
                    FullName = u.FullName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                })
                .ToListAsync();
        }

        public async Task ApproveLandlordAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) throw new KeyNotFoundException("User not found.");
            if (user.Role != UserRole.Landlord) throw new InvalidOperationException("User is not a landlord.");

            user.IsActive = true;
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task<UserPublicDto?> GetUserPublicAsync(int id)
        {
            return await _context.Users
                .Where(u => u.Id == id)
                .Select(u => new UserPublicDto { Id = u.Id, FullName = u.FullName, Email = u.Email, PhoneNumber = u.PhoneNumber })
                .FirstOrDefaultAsync();
        }

      
    }
}