using FU_House_Finder_Auth.Dtos;
using FU_House_Finder_Auth.Repositories.Interface;
using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(IUserRepository userRepository, IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
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

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            // Get user by email
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            // Check password (plain text comparison for now)
            if (user.PasswordHash != loginDto.Password)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            // Check if user is active
            if (!user.IsActive)
            {
                throw new InvalidOperationException("User account is inactive.");
            }

            // Generate tokens
            var accessToken = _jwtTokenService.GenerateAccessToken(user);
            var refreshToken = _jwtTokenService.GenerateRefreshToken();

            // Create response
            var response = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                User = new UserInfoDto
                {
                    Id = user.Id,
                    Email = user.Email,
                    FullName = user.FullName,
                    Role = user.Role.ToString()
                }
            };

            return response;
        }

        public async Task<LoginResponseDto> RefreshTokenAsync(string refreshToken)
        {
            // In a real application, you would validate the refresh token
            // and retrieve the user associated with it.
            // For now, we'll just generate new tokens.
            if (string.IsNullOrWhiteSpace(refreshToken))
            {
                throw new InvalidOperationException("Invalid refresh token.");
            }

            // Validate refresh token format (basic validation)
            try
            {
                Convert.FromBase64String(refreshToken);
            }
            catch
            {
                throw new InvalidOperationException("Invalid refresh token format.");
            }

            // In a real scenario, you would decode the refresh token to get the user ID
            // and validate it. For demonstration, we'll need additional implementation.
            // This is a simplified version that demonstrates the concept.
            throw new InvalidOperationException("Refresh token validation not fully implemented. Please decode token to get user ID.");
        }
    }
}
