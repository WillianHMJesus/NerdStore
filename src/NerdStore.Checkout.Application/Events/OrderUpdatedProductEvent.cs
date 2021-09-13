using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Events
{
    public class OrderUpdatedProductEvent : Event
    {
        public OrderUpdatedProductEvent(Guid clientId, Guid orderId, Guid productId, int quantity)
        {
            AggregateId = orderId;
            ClientId = clientId;
            OrderId = orderId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; set; }
    }
}
