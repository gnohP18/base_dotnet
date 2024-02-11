using base_dotnet.Databases.Entities;
using base_dotnet.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static base_dotnet.Common.Enum;

namespace base_dotnet.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Get list User
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return  await _userService.GetUsers();
        }

        /// <summary>
        /// Get user by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        [UserTypeFilter(UserRole.Admin, UserRole.User)]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userService.GetUserById(id);
            if (user is null) return NotFound();
            return Ok(user);
        }

        /// <summary>
        /// Create new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        [UserTypeFilter(UserRole.Admin, UserRole.User)]
        public async Task<ActionResult> Post([FromBody] User user)
        {
            await _userService.CreateUser(user);
            return Ok();
        }

        /// <summary>
        /// Edit User
        /// </summary>
        /// <param name="id">User id</param>
        /// <param name="user">User info update</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [UserTypeFilter(UserRole.Admin, UserRole.User)]
        public async Task<ActionResult> Put(int id, [FromBody] User user)
        {
            var currentUser = await _userService.GetUserById(id);
            if (currentUser is null) return NotFound();
            currentUser.Username = user.Username;
            currentUser.Email = user.Email;
            await _userService.UpdateUser(currentUser);
            return Ok();
        }

        /// <summary>
        /// Delete user by id
        /// </summary>
        /// <param name="id">User id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {            
            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}