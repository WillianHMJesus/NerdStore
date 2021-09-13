using NerdStore.Core.DomainObjects.DTOs;
using System;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class OrderConfirmedStockEvent : IntegrationEvent
    {
        public OrderConfirmedStockEvent(Guid orderId, Guid clientId, decimal amount, OrderProductListDTO items, string cardName, string cardNumber, string cardExpiration, string cardSecurityCode)
        {
            OrderId = orderId;
            ClientId = clientId;
            Amount = amount;
            Items = items;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardSecurityCode = cardSecurityCode;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Amount { get; private set; }
        public OrderProductListDTO Items { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardSecurityCode { get; private set; }
    }
}
