using FU_House_Finder_Auth.Dtos;
using FU_House_Finder_Auth.Repositories.Interface;
using FU_House_Finder_Auth.Repositories.Models;
using Microsoft.Extensions.Options;

namespace FU_House_Finder_Auth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenService _jwtTokenService;
        private readonly JwtSettings _jwtSettings;

        public UserService(
            IUserRepository userRepository,
            IJwtTokenService jwtTokenService,
            IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtTokenService = jwtTokenService;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<User> RegisterAsync(RegisterDto registerDto, UserRole role)
        {
            var userExists = await _userRepository.UserExistsAsync(registerDto.Email);
            if (userExists)
            {
                throw new InvalidOperationException($"Email '{registerDto.Email}' is already registered.");
            }

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

            return await _userRepository.RegisterUserAsync(user);
        }

        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);
            if (user == null)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            if (user.PasswordHash != loginDto.Password)
            {
                throw new InvalidOperationException("Invalid email or password.");
            }

            if (!user.IsActive)
            {
                throw new InvalidOperationException("User account is inactive.");
            }

            var accessToken = _jwtTokenService.GenerateAccessToken(user);
            var refreshTokenString = _jwtTokenService.GenerateRefreshToken();
            var expiryDate = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenExpirationDays);

            var response = new LoginResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshTokenString,
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

        public async Task<UserProfileDto> GetUserProfileAsync(int userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            var profile = new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                AvatarUrl = user.AvatarUrl
            };

            return profile;
        }

        public async Task<UserProfileDto> UpdateUserProfileAsync(int userId, ChangeProfileDto changeProfileDto)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                throw new InvalidOperationException("User not found.");
            }

            user.FullName = changeProfileDto.FullName;
            user.PhoneNumber = changeProfileDto.PhoneNumber;
            user.AvatarUrl = changeProfileDto.AvatarUrl;

            await _userRepository.UpdateUserAsync(user);

            var profile = new UserProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Role = user.Role,
                AvatarUrl = user.AvatarUrl
            };

            return profile;
        }

    }
}
