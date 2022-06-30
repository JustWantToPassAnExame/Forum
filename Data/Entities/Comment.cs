
namespace Data.Entities
{
    public class Comment:BaseEntity
    {
        public int UserId { get; set; }
        public int ThreadId { get; set; }
        public DateTime CreationDate { get; set; }
        public string CommentText { get; set; } = "";

        public virtual Thread_ Thread { get; set; }
        public virtual User Owner { get; set; }
    }
}
