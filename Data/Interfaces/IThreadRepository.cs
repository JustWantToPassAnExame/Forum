using Data.Entities;

namespace Data.Interfaces
{
    public interface IThreadRepository:IRepository<Thread_>
    {
        Task<IEnumerable<Thread_>> GetAllWithDetailsAsync();
        Task<Thread_> GetByIdWithDetailsAsync(int id);
    }
}
