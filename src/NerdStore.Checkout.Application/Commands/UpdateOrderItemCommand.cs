using NerdStore.Checkout.Application.Validations;
using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Commands
{
    public class UpdateOrderItemCommand : Command
    {
        public UpdateOrderItemCommand(Guid clientId, Guid productId, int quantity)
        {
            ClientId = clientId;
            ProductId = productId;
            Quantity = quantity;
        }

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }
        public int Quantity { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new UpdateOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
