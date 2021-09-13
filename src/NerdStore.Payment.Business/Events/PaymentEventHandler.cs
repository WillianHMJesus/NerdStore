using MediatR;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Payment.Business.DTOs;
using NerdStore.Payment.Business.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Payment.Business.Events
{
    public class PaymentEventHandler : INotificationHandler<OrderConfirmedStockEvent>
    {
        private readonly IPaymentService _paymentService;

        public PaymentEventHandler(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        public async Task Handle(OrderConfirmedStockEvent message, CancellationToken cancellationToken)
        {
            var paymentOrder = new PaymentOrderDTO
            {
                OrderId = message.OrderId,
                ClientId = message.ClientId,
                Amount = message.Amount,
                CardName = message.CardName,
                CardNumber = message.CardNumber,
                CardExpiration = message.CardExpiration,
                CardSecurityCode = message.CardSecurityCode
            };

            await _paymentService.MakePaymentOrder(paymentOrder);
        }
    }
}
