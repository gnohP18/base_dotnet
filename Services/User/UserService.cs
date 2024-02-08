using base_dotnet.Databases;
using base_dotnet.Databases.Entities;
using base_dotnet.Services;
using Microsoft.EntityFrameworkCore;

namespace base_dotnet.Services
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }

        public async Task CreateUser(User user)
        {
            user.UserType = await _context.UserTypes.FirstOrDefaultAsync(ut => ut.Id == user.UserTypeId);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetUserById(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<List<User>> GetUsers()
        {
            return await _context.Users.Take(50).ToListAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}