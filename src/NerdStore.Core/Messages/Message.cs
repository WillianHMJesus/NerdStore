using System;

namespace NerdStore.Core.Messages
{
    public abstract class Message
    {
        public Message()
        {
            MessageType = GetType().Name;
        }

        public Guid AggregateId { get; protected set; }
        public string MessageType { get; private set; }
    }
}
