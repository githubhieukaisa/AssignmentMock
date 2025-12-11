namespace FU_House_Finder_Auth.Repositories.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public string? AvatarUrl { get; set; }
        public bool IsActive { get; set; }
    }

    public enum UserRole
    {
        Admin,
        Staff,
        Landlord,
        Student
    }
}
