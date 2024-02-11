using base_dotnet.Common.Exceptions;
using base_dotnet.Databases;
using base_dotnet.Databases.Entities;
using base_dotnet.Services;
using Microsoft.AspNetCore.Mvc;
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
            try {

                if (checkExistUsername(user.Username, "username")) throw new BadRequestException("Username is existed");
                if (checkExistUsername(user.Email, "email")) throw new BadRequestException("Email is existed");

                user.UserType = await _context.UserTypes.FirstOrDefaultAsync(ut => ut.Id == user.UserTypeId);
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
            } catch {
                throw;
            }
        }

        public async Task DeleteUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user !=  null) {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            } else {
                throw new NotFoundException("User not found");
            }
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

        private bool checkExistUsername(string input, string column)
        {
            switch (column)
            {
                case "username":
                    return _context.Users.FirstOrDefault(x => x.Username == input) != null;
                case "email":
                    return _context.Users.FirstOrDefault(x => x.Email == input) != null;
                default:
                    return false;
            }
        }
    }
}