using System.Security.Cryptography;
using System.Text;
using base_dotnet.Databases.Entities;
using base_dotnet.DTOs;
using base_dotnet.Services;
using Microsoft.AspNetCore.Mvc;

namespace base_dotnet.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ITokenService _tokenService;

        public AuthController(
            IUserService userService,
            ITokenService tokenService)
        {
            _userService = userService;
            _tokenService = tokenService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        /// <param name="registerUser">AuthRegisterDto</param>
        /// <returns></returns>
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AuthRegisterDto registerUser)
        {
            registerUser.Username = registerUser.Username.ToLower();
            if (await _userService.GetUserByUsername(registerUser.Username) is not null)
                return BadRequest("Username already registered");            

            using var hashFunc = new HMACSHA256();
            var passwordBytes = Encoding.UTF8.GetBytes(registerUser.Password);

            var newUser = new User
            {
                Username = registerUser.Username,
                Email = registerUser.Email,
                UserTypeId = registerUser.UserType,
                PasswordHash = hashFunc.ComputeHash(passwordBytes),
                PasswordSalt = hashFunc.Key
            };
            await _userService.CreateUser(newUser);
            return Ok(_tokenService.GenerateToken(newUser));
        }

        /// <summary>
        /// Login for user
        /// </summary>
        /// <param name="loginUser">AuthLoginDto</param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AuthLoginDto loginUser)
        {
            loginUser.Username = loginUser.Username.ToLower();
            var existedUser = await _userService.GetUserByUsername(loginUser.Username);
            if (existedUser is null) return Unauthorized("User not found");
            using var hashFunc = new HMACSHA256(existedUser.PasswordSalt);
            var passwordBytes = Encoding.UTF8.GetBytes(loginUser.Password);
            var passwordHash = hashFunc.ComputeHash(passwordBytes);
            for (int i = 0; i < passwordHash.Length; i++)
            {
                if (passwordHash[i] != existedUser.PasswordHash[i])
                    return Unauthorized("Password not match");
            }
            return Ok(_tokenService.GenerateToken(existedUser));
        }
    }
}