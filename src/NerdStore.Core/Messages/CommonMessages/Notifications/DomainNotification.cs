using MediatR;
using System;

namespace NerdStore.Core.Messages.CommonMessages.Notifications
{
    public class DomainNotification : Message, INotification
    {
        public DomainNotification(string key, string value)
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.Now;
            Version = 1;
            Key = key;
            Value = value;
        }

        public Guid Id { get; private set; }
        public DateTime Timestamp { get; private set; }
        public string Key { get; private set; }
        public string Value { get; private set; }
        public int Version { get; private set; }
    }
}
