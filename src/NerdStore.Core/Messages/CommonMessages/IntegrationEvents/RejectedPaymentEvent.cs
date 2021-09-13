using System;

namespace NerdStore.Core.Messages.CommonMessages.IntegrationEvents
{
    public class RejectedPaymentEvent : IntegrationEvent
    {
        public RejectedPaymentEvent(Guid orderId, Guid clientId, Guid paymentId, Guid transactionId, decimal value)
        {
            OrderId = orderId;
            ClientId = clientId;
            PaymentId = paymentId;
            TransactionId = transactionId;
            Value = value;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public Guid PaymentId { get; private set; }
        public Guid TransactionId { get; private set; }
        public decimal Value { get; private set; }
    }
}
