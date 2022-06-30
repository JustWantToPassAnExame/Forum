using System.Runtime.Serialization;

namespace BLL.Validation
{
    [Serializable]
    public class ForumException:Exception
    {
        public ForumException(string message = "MarketException") : base(message)
        {

        }

        public ForumException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        protected ForumException(SerializationInfo info, StreamingContext context)
           : base(info, context)
        {
        }
    }
}
