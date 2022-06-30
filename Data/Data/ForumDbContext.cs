using Microsoft.EntityFrameworkCore;
using Data.Entities;

namespace Data.Data
{
    public class ForumDbContext : DbContext
    {

        public ForumDbContext()
        {
        }


       // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
         //   optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Forum;Trusted_Connection=True;");
       // }
        public ForumDbContext(DbContextOptions<ForumDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<CensorShip> CensorShips { get; set; }
        public DbSet<Thread_> Threads { get; set; }
        public DbSet<Topic> Topics { get; set; }

    }
}
