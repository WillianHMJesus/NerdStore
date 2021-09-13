using MediatR;
using NerdStore.Checkout.Application.Events;
using NerdStore.Checkout.Domain.Entities;
using NerdStore.Checkout.Domain.Interfaces;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects.DTOs;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Checkout.Application.Commands
{
    public class OrderCommandHandler :
        IRequestHandler<AddOrderItemCommand, bool>,
        IRequestHandler<UpdateOrderItemCommand, bool>,
        IRequestHandler<RemoveOrderItemCommand, bool>,
        IRequestHandler<ApplyVoucherOrderCommand, bool>,
        IRequestHandler<StartOrderCommand, bool>,
        IRequestHandler<FinishOrderCommand, bool>,
        IRequestHandler<CancelOrderReverseStockCommand, bool>,
        IRequestHandler<CancelOrderCommand, bool>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMediatorHandler _mediator;

        public OrderCommandHandler(IOrderRepository orderRepository,
            IMediatorHandler mediator)
        {
            _orderRepository = orderRepository;
            _mediator = mediator;
        }

        public async Task<bool> Handle(AddOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientAsync(message.ClientId);
            var orderItem = new OrderItem(message.ProductId, message.Name, message.Quantity, message.UnitValue);

            if (order == null)
            {
                order = new Order(message.ClientId);
                order.AddItem(orderItem);

                _orderRepository.Add(order);
                order.AddEvent(new DraftOrderStartedEvent(message.ClientId, message.ProductId));
            }
            else
            {
                order.AddItem(orderItem);
                if (order.OrderItemExisting(orderItem))
                {
                    _orderRepository.UpdateItem(order.OrderItems.FirstOrDefault(x => x.ProductId == orderItem.ProductId));
                }
                else
                {
                    _orderRepository.AddItem(orderItem);
                }

                order.AddEvent(new OrderUpdatedOrderEvent(message.ClientId, order.Id, order.Amount));
            }

            order.AddEvent(new OrderItemAddedEvent(message.ClientId, order.Id, message.ProductId, message.Name, message.UnitValue, message.Quantity));
            return await _orderRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(UpdateOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientAsync(message.ClientId);
            if (order == null)
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrderAsync(order.Id, message.ProductId);
            if (!order.OrderItemExisting(orderItem))
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Item do pedido não encontrado!"));
                return false;
            }

            order.UpdateUnitItem(orderItem, message.Quantity);
            _orderRepository.UpdateItem(orderItem);
            _orderRepository.Update(order);
            order.AddEvent(new OrderUpdatedProductEvent(message.ClientId, order.Id, message.ProductId, message.Quantity));
            order.AddEvent(new OrderUpdatedOrderEvent(message.ClientId, order.Id, order.Amount));

            return await _orderRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(RemoveOrderItemCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientAsync(message.ClientId);
            if (order == null)
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return false;
            }

            var orderItem = await _orderRepository.GetItemByOrderAsync(order.Id, message.ProductId);
            if (!order.OrderItemExisting(orderItem))
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Item do pedido não encontrado!"));
                return false;
            }

            order.RemoveItem(orderItem);
            _orderRepository.RemoveItem(orderItem);
            _orderRepository.Update(order);
            order.AddEvent(new OrderRemovedProductEvent(message.ClientId, order.Id, message.ProductId));
            order.AddEvent(new OrderUpdatedOrderEvent(message.ClientId, order.Id, order.Amount));

            return await _orderRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(ApplyVoucherOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientAsync(message.ClientId);
            if (order == null)
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Pedido não encontrado!"));
                return false;
            }

            var voucher = await _orderRepository.GetVoucherByCodeAsync(message.VoucherCode);
            if (voucher == null)
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Voucher não encontrado!"));
                return false;
            }

            if (!voucher.IsValid())
            {
                foreach (var error in message.ValidationResult.Errors)
                {
                    await _mediator.PublishNotificationAsync(new DomainNotification(error.ErrorCode, error.ErrorMessage));
                }

                return false;
            }

            order.ApplyVoucher(voucher);
            order.AddEvent(new VoucherAppliedOrderEvent(message.ClientId, order.Id, voucher.Id));
            order.AddEvent(new OrderUpdatedOrderEvent(message.ClientId, order.Id, order.Amount));
            _orderRepository.Update(order);

            return await _orderRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(StartOrderCommand message, CancellationToken cancellationToken)
        {
            if (!ValidateCommand(message)) return false;

            var order = await _orderRepository.GetOrderDraftByClientAsync(message.ClientId);
            order.StartOrder();

            var items = new List<OrderProductDTO>();
            order.OrderItems.ToList().ForEach(x => items.Add(new OrderProductDTO { Id = x.ProductId, Quantity = x.Quantity }));
            var orderProductList = new OrderProductListDTO { OrderId = order.Id, Items = items };

            order.AddEvent(new StartOrderEvent(order.Id, message.ClientId, order.Amount, orderProductList, message.CardName, message.CardNumber, message.CardExpiration,
                message.CardSecurityCode));

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(FinishOrderCommand message, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(message.OrderId);
            if (order == null)
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Pedido não encontrado"));
                return false;
            }

            order.FinalizeOrder();
            order.AddEvent(new FinalizedOrderEvent(message.OrderId));

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(CancelOrderReverseStockCommand message, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(message.OrderId);
            if (order == null)
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Pedido não encontrado"));
                return false;
            }

            var items = new List<OrderProductDTO>();
            order.OrderItems.ToList().ForEach(x => items.Add(new OrderProductDTO { Id = x.ProductId, Quantity = x.Quantity }));
            var orderProductList = new OrderProductListDTO { OrderId = order.Id, Items = items };

            order.CancelOrder();
            order.AddEvent(new CancelOrderEvent(message.OrderId, message.ClientId, orderProductList));

            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> Handle(CancelOrderCommand message, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByIdAsync(message.OrderId);
            if (order == null)
            {
                await _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, "Pedido não encontrado"));
                return false;
            }

            order.CancelOrder();
            _orderRepository.Update(order);
            return await _orderRepository.UnitOfWork.CommitAsync();
        }

        private bool ValidateCommand(Command message)
        {
            if (message.IsValid()) return true;

            foreach (var error in message.ValidationResult.Errors)
            {
                _mediator.PublishNotificationAsync(new DomainNotification(message.MessageType, error.ErrorMessage));
            }

            return false;
        }
    }
}
