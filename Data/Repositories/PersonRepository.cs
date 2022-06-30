using Data.Entities;
using Data.Interfaces;
using Data.Data;

namespace Data.Repositories
{
    public class PersonRepository:Repository<Person>, IPersonRepository
    {
        private readonly ForumDbContext context;
        public PersonRepository(ForumDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}
