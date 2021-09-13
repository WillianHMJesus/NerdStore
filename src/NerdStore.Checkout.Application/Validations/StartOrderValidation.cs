using FluentValidation;
using NerdStore.Checkout.Application.Commands;
using System;

namespace NerdStore.Checkout.Application.Validations
{
    public class StartOrderValidation : AbstractValidator<StartOrderCommand>
    {
        public StartOrderValidation()
        {
            RuleFor(x => x.ClientId).NotEqual(Guid.Empty)
                .WithMessage("Id do cliente inválido");

            RuleFor(x => x.OrderId).NotEqual(Guid.Empty)
                .WithMessage("Id do pedido inválido");

            RuleFor(x => x.CardName).NotEmpty().NotNull()
                .WithMessage("O nome do cartão não foi informado");

            RuleFor(x => x.CardNumber).CreditCard()
                .WithMessage("O número do cartão está inválido");

            RuleFor(x => x.CardExpiration).NotEmpty().NotNull()
                .WithMessage("A data de expiração do cartão não foi informada");

            RuleFor(x => x.CardSecurityCode).Length(3, 4)
                .WithMessage("O código de segurança do cartão está inválido");
        }
    }
}
