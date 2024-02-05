using base_dotnet.Databases.Entities;
using Microsoft.EntityFrameworkCore;

namespace base_dotnet.Databases
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<User> Users { get; set; } = null!;

        public DbSet<UserType> UserTypes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.UserType)
                .WithMany(t => t.Users)
                .HasForeignKey(u => u.UserTypeId);

            modelBuilder.Entity<UserType>()
                .Property(t => t.Name)
                .HasMaxLength(100);

            base.OnModelCreating(modelBuilder);
        }
    }
}