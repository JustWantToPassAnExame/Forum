using Data.Entities;

namespace Data.Interfaces
{
    public interface ICommentRepository: IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetAllWithDetailsAsync();
        Task<Comment> GetByIdWithDetailsAsync(int id);
    }
}
