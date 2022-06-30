using Data.Entities;
using Data.Interfaces;
using Data.Data;

namespace Data.Repositories
{
    public class CensorShipRepository: Repository<CensorShip>,ICensorShipRepository
    {
        private readonly ForumDbContext context;
        public CensorShipRepository(ForumDbContext context):base(context)
        {
            this.context = context;
        }
    }
}
