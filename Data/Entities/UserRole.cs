
namespace Data.Entities
{
    public class UserRole: BaseEntity
    {
        public string RoleName { get; set; } = "";
        public virtual ICollection<User> Users { get; set; }
    }
}
