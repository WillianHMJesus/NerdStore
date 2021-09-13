using FluentValidation;
using NerdStore.Checkout.Application.Commands;
using System;

namespace NerdStore.Checkout.Application.Validations
{
    public class AddOrderItemValidation : AbstractValidator<AddOrderItemCommand>
    {
        public AddOrderItemValidation()
        {
            RuleFor(x => x.ClientId).NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.ProductId).NotEqual(Guid.Empty)
                .WithMessage("Id do produto inválido");

            RuleFor(x => x.Name).NotEmpty().NotNull()
                .WithMessage("O nome do produto não foi informado");

            RuleFor(x => x.Quantity).GreaterThan(0)
                .WithMessage("A quantidade miníma de um item é 1");

            RuleFor(x => x.UnitValue).GreaterThan(0)
                .WithMessage("O valor precisa ser maior que 0");
        }
    }
}
