using FluentValidation;
using NerdStore.Checkout.Application.Commands;
using System;

namespace NerdStore.Checkout.Application.Validations
{
    public class RemoveOrderItemValidation : AbstractValidator<RemoveOrderItemCommand>
    {
        public RemoveOrderItemValidation()
        {
            RuleFor(x => x.ClientId).NotEqual(Guid.Empty)
               .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ProductId).NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");
        }
    }
}
