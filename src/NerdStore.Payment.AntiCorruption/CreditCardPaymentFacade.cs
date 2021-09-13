using NerdStore.Payment.Business.DTOs;
using NerdStore.Payment.Business.Entities;
using NerdStore.Payment.Business.Entities.Enums;
using NerdStore.Payment.Business.Interfaces;

namespace NerdStore.Payment.AntiCorruption
{
    public class CreditCardPaymentFacade : ICreditCardPaymentFacade
    {
        private readonly IConfigurationManager _configuration;
        private readonly IPayPalGateway _payPalGateway;

        public CreditCardPaymentFacade(IConfigurationManager configuration,
            IPayPalGateway payPalGateway)
        {
            _configuration = configuration;
            _payPalGateway = payPalGateway;
        }

        public Transaction MakePayment(OrderDTO order, Business.Entities.Payment payment)
        {
            var apiKey = _configuration.GetValue("apiKey");
            var encriptionKey = _configuration.GetValue("encriptionKey");

            var serviceKey = _payPalGateway.GetServiceKey(apiKey, encriptionKey);
            var cardHashKey = _payPalGateway.GetCardHashKey(serviceKey, payment.CardNumber);
            var paymentResult = _payPalGateway.CommitTransaction(cardHashKey, order.Id.ToString(), payment.Value);

            var transaction = new Transaction(order.Id, payment.Id, order.Value);

            if (paymentResult)
            {
                transaction.SetStatus(TransactionStatus.Paid);
                return transaction;
            }

            transaction.SetStatus(TransactionStatus.Rejected);
            return transaction;
        }
    }
}
