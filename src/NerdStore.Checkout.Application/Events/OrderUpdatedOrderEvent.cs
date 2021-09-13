using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Events
{
    public class OrderUpdatedOrderEvent : Event
    {
        public OrderUpdatedOrderEvent(Guid clientId, Guid orderId, decimal amount)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            Amount = amount;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public decimal Amount { get; private set; }
    }
}
