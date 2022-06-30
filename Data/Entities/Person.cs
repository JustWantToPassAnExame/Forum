
namespace Data.Entities
{
    public class Person: BaseEntity
    {
        public DateTime BirthDate { get; set; }
        public string PersonInfo { get; set; } = "";      

    }
}
