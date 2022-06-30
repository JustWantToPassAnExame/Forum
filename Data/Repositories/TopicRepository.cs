using Data.Entities;
using Data.Interfaces;
using Data.Data;

namespace Data.Repositories
{
    public class TopicRepository: Repository<Topic>, ITopicRepository
    {
        private readonly ForumDbContext context;
        public TopicRepository(ForumDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
