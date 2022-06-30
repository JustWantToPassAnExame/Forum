using Microsoft.EntityFrameworkCore;
using Data.Entities;
using Data.Interfaces;
using Data.Data;

namespace Data.Repositories
{
    public class ThreadRepository: Repository<Thread_>, IThreadRepository
    {
        private readonly ForumDbContext context;
        public ThreadRepository(ForumDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Thread_>> GetAllWithDetailsAsync()
        {
            return await context.Threads
                .Include(t => t.Owner)
                .Include(t => t.CensorShip)
                .Include(t => t.Topic)
                .Include(t => t.Coments).ThenInclude(c => c.Owner)
                .ToListAsync();
        }

        public async Task<Thread_> GetByIdWithDetailsAsync(int id)
        {
            return await context.Threads
                .Include(t => t.Owner)
                .Include(t => t.CensorShip)
                .Include(t => t.Topic)
                .Include(t => t.Coments).ThenInclude(c => c.Owner)
                .FirstOrDefaultAsync(x=>x.Id==id);
        }
    }
}
