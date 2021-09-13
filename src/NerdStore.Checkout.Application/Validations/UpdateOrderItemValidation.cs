using FluentValidation;
using NerdStore.Checkout.Application.Commands;
using System;

namespace NerdStore.Checkout.Application.Validations
{
    public class UpdateOrderItemValidation : AbstractValidator<UpdateOrderItemCommand>
    {
        public UpdateOrderItemValidation()
        {
            RuleFor(x => x.ClientId).NotEqual(Guid.Empty)
               .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ProductId).NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(x => x.Quantity).GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");
        }
    }
}
