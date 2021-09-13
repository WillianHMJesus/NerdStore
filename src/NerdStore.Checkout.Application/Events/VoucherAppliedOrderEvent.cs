using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Events
{
    public class VoucherAppliedOrderEvent : Event
    {
        public VoucherAppliedOrderEvent(Guid clientId, Guid orderId, Guid voucherId)
        {
            ClientId = clientId;
            OrderId = orderId;
            VoucherId = voucherId;
        }

        public Guid ClientId { get; private set; }
        public Guid OrderId { get; private set; }
        public Guid VoucherId { get; private set; }
    }
}
