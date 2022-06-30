
namespace Data.Interfaces
{
    public interface IUnitOfWork
    {
        ICensorShipRepository CensorShipRepository { get; }
        ICommentRepository CommentRepository { get; }
        IPersonRepository PersonRepository { get; }
        IThreadRepository ThreadRepository { get; }
        ITopicRepository TopicRepository { get; }
        IUserRepository UserRepository { get; }
        IUserRoleRepository UserRoleRepository { get; }
        int Save();
    }
}
