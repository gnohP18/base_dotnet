namespace base_dotnet.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }

        public string Username { get; set; } = null!;

        public string Email { get; set; } = null!;

        public int UserTypeId { get; set; }

        public string? UserTypeName { get; set; } 
    }
}