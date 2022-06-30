using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Interfaces;
using Data.Data;

namespace Data.Repositories
{
    public class UserRepository:Repository<User>, IUserRepository
    {
        private readonly ForumDbContext context;
        public UserRepository(ForumDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<User>> GetAllWithDetailsAsync()
        {
            return await context.Users
                .Include(u => u.Person)
                .Include(u => u.UserRole)
                .Include(u => u.Threads)
                .ToListAsync();
        }

        public async Task<User> GetByIdWithDetailsAsync(int id)
        {
            return await context.Users
                .Include(u => u.Threads)
                .Include(u => u.Person)
                .Include(u => u.UserRole)
                .FirstOrDefaultAsync(u => u.Id == id);

        }
    }
}
