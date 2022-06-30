using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Models
{
    public class TopicModel
    {
        public int Id { get; set; }
#pragma warning disable CS8618 // Non-nullable property 'TopicName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public string TopicName { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'TopicName' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
#pragma warning disable CS8618 // Non-nullable property 'ThreadsIds' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
        public virtual ICollection<int> ThreadsIds { get; set; }
#pragma warning restore CS8618 // Non-nullable property 'ThreadsIds' must contain a non-null value when exiting constructor. Consider declaring the property as nullable.
    }
}
