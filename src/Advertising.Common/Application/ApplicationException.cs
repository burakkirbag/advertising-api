using System;
using System.Runtime.Serialization;

namespace Advertising.Application
{
    [Serializable]
    public abstract class ApplicationException : AdvertisingException
    {
        protected ApplicationException()
        {
        }

        protected ApplicationException(string message) : base(message)
        {
        }

        protected ApplicationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        protected ApplicationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}