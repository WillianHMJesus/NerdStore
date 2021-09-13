using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Events
{
    public class DraftOrderStartedEvent : Event
    {
        public DraftOrderStartedEvent(Guid clientId, Guid orderId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
    }
}
