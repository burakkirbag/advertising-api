using System;

namespace Advertising.Bus
{
    public abstract class BusMessageBase : IBusMessage
    {
        private string _messageId;
        public string MessageId => GenerateMessageId();

        public virtual string GenerateMessageId()
        {
            if (string.IsNullOrEmpty(_messageId))
                _messageId = Guid.NewGuid().ToString();

            return _messageId;
        }
    }
}
