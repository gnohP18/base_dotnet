using base_dotnet.Databases.Entities;

namespace base_dotnet.Services
{
    public interface IUserService
    {
        Task<List<User?>> GetUsers();
        Task<User?> GetUserById(int id);
        Task<User?> GetUserByUsername(string username);
        Task CreateUser(User user);
        Task UpdateUser(User user);
        Task DeleteUser(User user);
    }
}