using FluentValidation;
using NerdStore.Checkout.Application.Commands;
using System;

namespace NerdStore.Checkout.Application.Validations
{
    public class ApplyVoucherOrderValidation : AbstractValidator<ApplyVoucherOrderCommand>
    {
        public ApplyVoucherOrderValidation()
        {
            RuleFor(x => x.ClientId).NotEqual(Guid.Empty)
               .WithMessage("Id do cliente inválido");

            RuleFor(x => x.VoucherCode).NotNull().NotEmpty()
                .WithMessage("Código de voucher inválido");
        }
    }
}
