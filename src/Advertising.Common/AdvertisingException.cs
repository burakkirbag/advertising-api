using System;
using System.Runtime.Serialization;

namespace Advertising
{
    public class AdvertisingException : Exception
    {
        public AdvertisingException()
        {
        }

        public AdvertisingException(string message) : base(message)
        {
        }

        public AdvertisingException(string message, Exception innerException) : base(message, innerException)
        {
        }

        protected AdvertisingException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}
