
namespace Data.Entities
{
    public class User: BaseEntity
    {

        public int UserRoleId { get; set; }
        public int PersonId { get; set; }
        public string NickName { get; set; } = "";
        public string Login { get; set; } = "";
        public string Password { get; set; } = "";

        public virtual Person Person { get; set; }
        public virtual UserRole UserRole { get; set; }
        public virtual ICollection<Thread_> Threads { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }


    }
}
