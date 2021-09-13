using MediatR;
using Microsoft.AspNetCore.Mvc;
using NerdStore.Catalog.Application.Services;
using NerdStore.Checkout.Application.Commands;
using NerdStore.Checkout.Application.Queries;
using NerdStore.Checkout.Application.Queries.DTOs;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.API.Controllers
{
    public class CartsController : BaseController
    {
        private readonly IProductAppService _productAppService;
        private readonly IOrderQueries _orderQueries;
        private readonly IMediatorHandler _mediator;

        public CartsController(IProductAppService productAppService,
            IOrderQueries orderQueries,
            IMediatorHandler mediator,
            INotificationHandler<DomainNotification> notifications)
            : base(notifications, mediator)
        {
            _productAppService = productAppService;
            _orderQueries = orderQueries;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            return Ok(await _orderQueries.GetCartByClientAsync(ClientId));
        }

        [HttpPost("add-item")]
        public async Task<IActionResult> AddItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest(new
                {
                    Errors = new List<string> { "Produto não encontrado." }
                });
            }

            if (product.Quantity < quantity)
            {
                return BadRequest(new
                {
                    Errors = new List<string> { "Produto com estoque insuficiente" }
                });
            }

            var command = new AddOrderItemCommand(ClientId, product.Id, product.Name, product.Quantity, product.Value);
            await _mediator.SendCommandAsync(command);

            if (OperationIsValid())
            {
                return Ok();
            }

            return BadRequest(new { Errors = GetNotificationsErrors() });
        }

        [HttpPost("remove-item")]
        public async Task<IActionResult> RemoveItem(Guid id)
        {
            var product = await _productAppService.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest(new
                {
                    Errors = new List<string> { "Produto não encontrado." }
                });
            }

            var command = new RemoveOrderItemCommand(ClientId, id);
            await _mediator.SendCommandAsync(command);

            if (OperationIsValid())
            {
                return Ok();
            }

            return BadRequest(new { Errors = GetNotificationsErrors() });
        }

        [HttpPut("update-item")]
        public async Task<IActionResult> UpdateItem(Guid id, int quantity)
        {
            var product = await _productAppService.GetByIdAsync(id);
            if (product == null)
            {
                return BadRequest(new
                {
                    Errors = new List<string> { "Produto não encontrado." }
                });
            }

            var command = new UpdateOrderItemCommand(ClientId, id, quantity);
            await _mediator.SendCommandAsync(command);

            if (OperationIsValid())
            {
                return Ok();
            }

            return BadRequest(new { Errors = GetNotificationsErrors() });
        }

        [HttpPost("apply-voucher")]
        public async Task<IActionResult> ApplyVoucher(string voucherCode)
        {
            var command = new ApplyVoucherOrderCommand(ClientId, voucherCode);
            await _mediator.SendCommandAsync(command);

            if (OperationIsValid())
            {
                return Ok();
            }

            return BadRequest(new { Errors = GetNotificationsErrors() });
        }

        [HttpPost("start-order")]
        public async Task<IActionResult> StartOrder(CartPaymentDTO payment)
        {
            var cart = await _orderQueries.GetCartByClientAsync(ClientId);
            if (cart == null)
            {
                return BadRequest(new
                {
                    Errors = new List<string> { "Carrinho está vazio." }
                });
            }

            var command = new StartOrderCommand(cart.OrderId, ClientId, cart.Amount, payment.CardName, payment.CardNumber, 
                payment.CardExpiration, payment.CardSecurityCode);
            await _mediator.SendCommandAsync(command);

            if (OperationIsValid())
            {
                return Ok();
            }

            return BadRequest(new { Errors = GetNotificationsErrors() });
        }
    }
}
