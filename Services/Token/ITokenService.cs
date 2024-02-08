using base_dotnet.Databases.Entities;

namespace base_dotnet.Services
{
    public interface ITokenService
    {
        /// <summary>
        /// Generate token => Claim Id, Email
        /// Expire time : 1 day
        /// </summary>
        /// <param name="user">User Login/Register</param>
        /// <returns></returns>
        string GenerateToken(User user);

        /// <summary>
        /// Validate token 
        /// </summary>
        /// <param name="token">token from HTTP request</param>
        /// <returns></returns>
        int? ValidateToken(string token);
    }
}