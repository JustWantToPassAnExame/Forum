using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class UserRoleModel
    {
        public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'RoleName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string RoleName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'RoleName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'UsersIds' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public virtual ICollection<int> UsersIds { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'UsersIds' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    }
}
