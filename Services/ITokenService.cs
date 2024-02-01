using base_dotnet.Databases.Entities;

namespace base_dotnet.Services
{
    public interface ITokenService
    {
        string GenerateToken(User user);
        int? ValidateToken(string token);
    }
}