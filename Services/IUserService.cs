using base_dotnet.Databases.Entities;

namespace base_dotnet.Services
{
    public interface IUserService
    {
        List<User> GetUsers();
        User? GetUserById(int id);
        User? GetUserByUsername(string username);
        void CreateUser(User user);
        void UpdateUser(User user);
        void DeleteUser(User user);
    }
}