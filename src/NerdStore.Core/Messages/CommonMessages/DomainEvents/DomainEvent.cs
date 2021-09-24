using MediatR;
using System;

namespace NerdStore.Core.Messages.CommonMessages.DomainEvents
{
    public class DomainEvent : Message, INotification
    {
        public DomainEvent(Guid aggregateId)
        {
            AggregateId = aggregateId;
            Timestamp = DateTime.Now;
        }

        public DateTime Timestamp { get; private set; }
    }
}
