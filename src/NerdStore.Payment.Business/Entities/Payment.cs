using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Payment.Business.Entities
{
    public class Payment : Entity, IAggregateRoot
    {
        public Payment(Guid orderId, decimal value, string cardName, string cardNumber, string cardExpiration, string cardSecurityCode)
        {
            OrderId = orderId;
            Value = value;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardSecurityCode = cardSecurityCode;
        }

        public Guid OrderId { get; private set; }
        public string Status { get; private set; }
        public decimal Value { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardSecurityCode { get; private set; }
        public Transaction Transaction { get; private set; }
    }
}
