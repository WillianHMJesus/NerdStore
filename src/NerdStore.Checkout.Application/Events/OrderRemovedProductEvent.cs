using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Events
{
    public class OrderRemovedProductEvent : Event
    {
        public OrderRemovedProductEvent(Guid clientId, Guid orderId, Guid productId)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            ProductId = productId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
    }
}
