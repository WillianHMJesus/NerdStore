using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Events
{
    public class FinalizedOrderEvent : Event
    {
        public FinalizedOrderEvent(Guid orderId)
        {
            AggregateId = orderId;
            OrderId = orderId;
        }

        public Guid OrderId { get; private set; }
    }
}
