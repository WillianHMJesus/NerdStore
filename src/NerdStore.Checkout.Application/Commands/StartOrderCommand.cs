using NerdStore.Checkout.Application.Validations;
using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Commands
{
    public class StartOrderCommand : Command
    {
        public StartOrderCommand(Guid orderId, Guid clientId, decimal amount, string cardName, string cardNumber, string cardExpiration, string cardSecurityCode)
        {
            OrderId = orderId;
            ClientId = clientId;
            Amount = amount;
            CardName = cardName;
            CardNumber = cardNumber;
            CardExpiration = cardExpiration;
            CardSecurityCode = cardSecurityCode;
        }

        public Guid OrderId { get; private set; }
        public Guid ClientId { get; private set; }
        public decimal Amount { get; private set; }
        public string CardName { get; private set; }
        public string CardNumber { get; private set; }
        public string CardExpiration { get; private set; }
        public string CardSecurityCode { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new StartOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
