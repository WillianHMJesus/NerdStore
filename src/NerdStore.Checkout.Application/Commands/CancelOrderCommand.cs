using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Commands
{
    public class CancelOrderCommand : Command
    {
        public CancelOrderCommand(Guid orderId, Guid clientId)
        {
            AggregateId = orderId;
            OrderId = orderId;
            ClientId = clientId;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
    }
}
