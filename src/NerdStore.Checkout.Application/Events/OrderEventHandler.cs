using MediatR;
using NerdStore.Checkout.Application.Commands;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Checkout.Application.Events
{
    public class OrderEventHandler :
        INotificationHandler<DraftOrderStartedEvent>,
        INotificationHandler<OrderUpdatedOrderEvent>,
        INotificationHandler<OrderItemAddedEvent>,
        INotificationHandler<OrderUpdatedProductEvent>,
        INotificationHandler<OrderRemovedProductEvent>,
        INotificationHandler<VoucherAppliedOrderEvent>,
        INotificationHandler<OrderRejectedStockEvent>,
        INotificationHandler<RealizedPaymentEvent>,
        INotificationHandler<RejectedPaymentEvent>
    {

        private readonly IMediatorHandler _mediator;

        public OrderEventHandler(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        public Task Handle(DraftOrderStartedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedOrderEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderItemAddedEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderUpdatedProductEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(OrderRemovedProductEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public Task Handle(VoucherAppliedOrderEvent notification, CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }

        public async Task Handle(OrderRejectedStockEvent message, CancellationToken cancellationToken)
        {
            await _mediator.SendCommandAsync(new CancelOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(RealizedPaymentEvent message, CancellationToken cancellationToken)
        {
            await _mediator.SendCommandAsync(new FinishOrderCommand(message.OrderId, message.ClientId));
        }

        public async Task Handle(RejectedPaymentEvent message, CancellationToken cancellationToken)
        {
            await _mediator.SendCommandAsync(new CancelOrderReverseStockCommand(message.OrderId, message.ClientId));
        }
    }
}
