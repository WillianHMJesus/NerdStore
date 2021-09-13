using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Payment.Business.DTOs;
using NerdStore.Payment.Business.Entities;
using NerdStore.Payment.Business.Entities.Enums;
using NerdStore.Payment.Business.Interfaces;
using System.Threading.Tasks;
using PaymentEntity = NerdStore.Payment.Business.Entities.Payment;

namespace NerdStore.Payment.Business.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly ICreditCardPaymentFacade _creditCardPaymentFacade;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IMediatorHandler _mediator;

        public PaymentService(ICreditCardPaymentFacade creditCardPaymentFacade, 
            IPaymentRepository paymentRepository, 
            IMediatorHandler mediator)
        {
            _creditCardPaymentFacade = creditCardPaymentFacade;
            _paymentRepository = paymentRepository;
            _mediator = mediator;
        }

        public async Task<Transaction> MakePaymentOrder(PaymentOrderDTO paymentOrder)
        {
            var order = new OrderDTO
            {
                Id = paymentOrder.OrderId,
                Value = paymentOrder.Amount
            };

            var payment = new PaymentEntity
            (
                paymentOrder.OrderId,
                paymentOrder.Amount,
                paymentOrder.CardName,
                paymentOrder.CardNumber,
                paymentOrder.CardExpiration,
                paymentOrder.CardSecurityCode
            );

            var transaction = _creditCardPaymentFacade.MakePayment(order, payment);

            if(transaction.TransactionStatus == TransactionStatus.Paid)
            {
                _paymentRepository.Add(payment);
                _paymentRepository.AddTransaction(transaction);

                await _paymentRepository.UnitOfWork.CommitAsync();
                payment.AddEvent(new RealizedPaymentEvent(order.Id, paymentOrder.ClientId, transaction.PaymentId, transaction.Id, order.Value));

                return transaction;
            }

            await _mediator.PublishNotificationAsync(new DomainNotification("pagamento", "A operadora recusou o pagamento"));
            await _mediator.PublishEventAsync(new RejectedPaymentEvent(order.Id, paymentOrder.ClientId, transaction.PaymentId, transaction.Id, order.Value));

            return transaction;
        }
    }
}
