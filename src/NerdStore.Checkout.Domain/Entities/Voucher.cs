using FluentValidation;
using NerdStore.Checkout.Domain.Entities.Enums;
using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Checkout.Domain.Entities
{
    public class Voucher : Entity
    {
        protected Voucher() { }

        public string Code { get; private set; }
        public decimal? Percentage { get; private set; }
        public decimal? DiscountValue { get; private set; }
        public DiscountType DiscountType { get; private set; }
        public DateTime Create { get; private set; }
        public DateTime Use { get; private set; }
        public DateTime Expiration { get; private set; }
        public bool Used { get; private set; }
        public bool Active { get; private set; }
        public Order Order { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new VoucherValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }

    public class VoucherValidation : AbstractValidator<Voucher>
    {
        public VoucherValidation()
        {
            RuleFor(x => x.Expiration).Must(ExpirationBiggerThanNow)
                .WithMessage("Voucher expirado!");

            RuleFor(x => x.Active).Equal(true)
                .WithMessage("Voucher inativo!");

            RuleFor(x => x.Used).Equal(false)
                .WithMessage("Voucher já foi utilizado!");
        }

        protected static bool ExpirationBiggerThanNow(DateTime expiration)
        {
            return expiration >= DateTime.Now;
        }
    }
}
