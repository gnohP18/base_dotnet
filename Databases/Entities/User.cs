using System.ComponentModel.DataAnnotations;

namespace base_dotnet.Databases.Entities
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [StringLength(100)]
        public string Username { get; set; } = null!;

        [StringLength(255)]
        public string Email { get; set; } = null!;

        public byte[] PasswordHash { get; set; } = null!;

        public byte[] PasswordSalt { get; set; } = null!;

        public int UserTypeId { get; set; }

        public virtual UserType UserType { get; set; } = null!;
    }
}