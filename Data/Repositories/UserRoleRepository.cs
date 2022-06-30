using Data.Entities;
using Data.Data;
using Data.Interfaces;
namespace Data.Repositories
{
    public class UserRoleRepository:Repository<UserRole>, IUserRoleRepository
    {
        private readonly ForumDbContext context;
        public UserRoleRepository(ForumDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
