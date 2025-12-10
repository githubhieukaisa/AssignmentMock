using FU_House_Finder_Auth.Dtos;
using FU_House_Finder_Auth.Repositories.Interface;
using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterAsync(RegisterDto registerDto, UserRole role)
        {
            // Check if user already exists
            var userExists = await _userRepository.UserExistsAsync(registerDto.Email);
            if (userExists)
            {
                throw new InvalidOperationException($"Email '{registerDto.Email}' is already registered.");
            }

            // Create new user
            var user = new User
            {
                FullName = registerDto.FullName,
                Email = registerDto.Email,
                PasswordHash = registerDto.Password,
                PhoneNumber = registerDto.PhoneNumber,
                Role = role,
                AvatarUrl = null,
                IsActive = true
            };

            // Register user in database
            return await _userRepository.RegisterUserAsync(user);
        }
    }
}
