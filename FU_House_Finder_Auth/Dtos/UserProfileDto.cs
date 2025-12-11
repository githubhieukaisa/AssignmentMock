using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Dtos
{
    public class UserProfileDto
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? AvatarUrl { get; set; }
    }
}
