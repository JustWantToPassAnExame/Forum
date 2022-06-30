
namespace Data.Entities
{
    public class Topic : BaseEntity
    {
        public string TopicName { get; set; } = "";
        public virtual ICollection<Thread_> Threads { get; set; }
    }
}
