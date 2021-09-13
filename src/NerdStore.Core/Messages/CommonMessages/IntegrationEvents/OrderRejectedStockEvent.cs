using System;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderRejectedStockEvent : IntegrationEvent
    {
        public OrderRejectedStockEvent(Guid orderId, Guid clientId)
        {
            OrderId = orderId;
            ClientId = clientId;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
    }
}
