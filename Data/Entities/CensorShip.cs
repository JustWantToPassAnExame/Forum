
namespace Data.Entities
{
    public class CensorShip: BaseEntity
    {
        public string CensorStatus { get; set; } = "";

        public virtual ICollection<Thread_> Threads { get; set; }
    }
}
