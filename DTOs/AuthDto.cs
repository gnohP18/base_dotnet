using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using base_dotnet.Databases.Entities;

namespace base_dotnet.DTOs
{
    public class AuthRegisterDto
    {
        [MaxLength(100)]
        [DefaultValue("user")]
        public string Username { get; set; } = null!;        

        [EmailAddress]
        [MaxLength(100)]
        [DefaultValue("user@test.com")]
        public string Email { get; set; } = null!;

        [DefaultValue("2")]
        public int UserType { get; set; }

        [MaxLength(100)]
        [DefaultValue("user")]
        public string Password { get; set; } = null!;
    }
    
    public class AuthLoginDto
    {
        [MaxLength(100)]
        [DefaultValue("admin")]
        public string Username { get; set; } = null!;

        [MaxLength(100)]
        [DefaultValue("admin")]
        public string Password { get; set; } = null!;
    }
}