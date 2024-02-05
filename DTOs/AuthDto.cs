using System.ComponentModel.DataAnnotations;
using base_dotnet.Databases.Entities;

namespace base_dotnet.DTOs
{
    public class AuthRegisterDto
    {
        [MaxLength(100)]
        public string Username { get; set; } = null!;        

        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = null!;

        public int UserType { get; set; }

        [MaxLength(100)]
        public string Password { get; set; } = null!;
    }
    
    public class AuthLoginDto
    {
        [MaxLength(100)]
        public string Username { get; set; } = null!;

        [MaxLength(100)]
        public string Password { get; set; } = null!;
    }
}