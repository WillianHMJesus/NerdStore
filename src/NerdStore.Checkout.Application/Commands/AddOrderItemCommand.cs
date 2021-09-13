using NerdStore.Checkout.Application.Validations;
using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Commands
{
    public class AddOrderItemCommand : Command
    {
        public AddOrderItemCommand(Guid clientId, Guid productId, string name, int quantity, decimal unitValue)
        {
            ClientId = clientId;
            ProductId = productId;
            Name = name;
            Quantity = quantity;
            UnitValue = unitValue;
        }

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public string Name { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitValue { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new AddOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}