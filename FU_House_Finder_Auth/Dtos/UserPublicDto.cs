namespace FU_House_Finder_Auth.Dtos
{
    public class UserPublicDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}
