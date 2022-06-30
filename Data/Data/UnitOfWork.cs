using Data.Interfaces;
using Data.Repositories;
namespace Data.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ForumDbContext _context;
        public UnitOfWork(ForumDbContext context)
        {
            this._context = context;
        }
        public ICensorShipRepository CensorShipRepository => new CensorShipRepository(_context);

        public ICommentRepository CommentRepository => new CommentRepository(_context);

        public IPersonRepository PersonRepository => new PersonRepository(_context);

        public IThreadRepository ThreadRepository => new ThreadRepository(_context);

        public ITopicRepository TopicRepository => new TopicRepository(_context);

        public IUserRepository UserRepository => new UserRepository(_context);

        public IUserRoleRepository UserRoleRepository => new UserRoleRepository(_context);


        public int Save()
        {
            return _context.SaveChanges();
        }
    }
}
