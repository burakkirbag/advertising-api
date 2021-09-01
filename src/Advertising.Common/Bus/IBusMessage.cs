using System;

namespace Advertising.Bus
{
    public interface IBusMessage
    {
        public string MessageId { get; }

        string GenerateMessageId();
    }
}
