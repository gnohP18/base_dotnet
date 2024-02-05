namespace base_dotnet.Databases.Entities
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual List<User> Users { get; set; }
        
        public UserType()
        {
            Users = new List<User>();
        }
    }
}