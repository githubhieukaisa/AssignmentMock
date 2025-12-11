using FU_House_Finder_Auth.Repositories.Models;

namespace FU_House_Finder_Auth.Dtos
{
    public class ChangeProfileDto
    {
        public string FullName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; }
    }
}
