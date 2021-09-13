using NerdStore.Checkout.Application.Validations;
using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Commands
{
    public class RemoveOrderItemCommand : Command
    {
        public RemoveOrderItemCommand(Guid clientId, Guid productId)
        {
            ClientId = clientId;
            ProductId = productId;
        }

        public Guid ClientId { get; private set; }
        public Guid ProductId { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new RemoveOrderItemValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
