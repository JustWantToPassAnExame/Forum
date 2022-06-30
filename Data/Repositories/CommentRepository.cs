using Data.Entities;
using Data.Interfaces;
using Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
{
    public class CommentRepository:Repository<Comment>, ICommentRepository
    {
        private readonly ForumDbContext context;
        public CommentRepository(ForumDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Comment>> GetAllWithDetailsAsync()
        {
            return await context.Comments
                .Include(c => c.Owner).ToListAsync();
        }

        public async Task<Comment> GetByIdWithDetailsAsync(int id)
        {
            return await context.Comments
                .Include(с => с.Owner)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
