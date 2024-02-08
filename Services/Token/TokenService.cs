using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using base_dotnet.Databases.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace base_dotnet.Services
{
    public class TokenService : ITokenService
    {
        private readonly string _jwtSecretKey;
        private readonly IOptionsMonitor<JwtBearerOptions> _jwtOptions;

        public TokenService(IConfiguration configuration, IOptionsMonitor<JwtBearerOptions> jwtOptions)
        {
            _jwtSecretKey = configuration["JwtSecretKey"] ?? string.Empty;
            _jwtOptions = jwtOptions;
        }

        public string GenerateToken(User user)
        {
            var claims = new List<Claim>();
            claims.AddRange(new[] {
                new Claim("Id", user.Id.ToString()) ,
                new Claim("Email", user.Email) ,
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            });

            var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSecretKey));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new(symmetricKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        public int? ValidateToken(string token)
        {
            try
            {
                if (string.IsNullOrEmpty(token))
                {
                    return null;
                }
                
                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenValidationParameters = _jwtOptions.Get(JwtBearerDefaults.AuthenticationScheme).TokenValidationParameters;
                tokenHandler.ValidateToken(token,
                    tokenValidationParameters
                , out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "Id").Value);
                return userId;
            }
            catch
            {
                throw;
                return null;
            }
        }


    }
}