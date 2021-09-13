using NerdStore.Core.DomainObjects;
using NerdStore.Payment.Business.Entities.Enums;
using System;

namespace NerdStore.Payment.Business.Entities
{
    public class Transaction : Entity
    {
        public Transaction(Guid orderId, Guid paymentId, decimal total)
        {
            OrderId = orderId;
            PaymentId = paymentId;
            Total = total;
        }

        public Guid OrderId { get; private set; }
        public Guid PaymentId { get; private set; }
        public decimal Total { get; private set; }
        public TransactionStatus TransactionStatus { get; private set; }
        public Payment Payment { get; private set; }

        public void SetStatus(TransactionStatus transactionStatus)
        {
            TransactionStatus = transactionStatus;
        }
    }
}
