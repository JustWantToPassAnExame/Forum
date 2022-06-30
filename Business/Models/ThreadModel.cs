using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class ThreadModel
    {
        public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'UserId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string UserId { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'UserId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public int TopicId { get; set; }
        public int CensorShipId { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'UserNickName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string UserNickName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'UserNickName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public DateTime CreationDate { get; set; }
        public int Vievs { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'ThreadTitle' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string ThreadTitle { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ThreadTitle' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'ThreadBody' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string ThreadBody { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ThreadBody' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

#pragma warning disable CS8618 // Non-nullable property 'CommentsId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public virtual ICollection<int> CommentsId { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'CommentsId' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.

    }
}
