using NerdStore.Core.DomainObjects.DTOs;
using System;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class CancelOrderEvent : IntegrationEvent
    {
        public CancelOrderEvent(Guid orderId, Guid clientId, OrderProductListDTO productsOrder)
        {
            OrderId = orderId;
            ClientId = clientId;
            ProductsOrder = productsOrder;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public OrderProductListDTO ProductsOrder { get; private set; }
    }
}
