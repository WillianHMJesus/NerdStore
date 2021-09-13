using NerdStore.Checkout.Application.Validations;
using NerdStore.Core.Messages;
using System;

namespace NerdStore.Checkout.Application.Commands
{
    public class ApplyVoucherOrderCommand : Command
    {
        public ApplyVoucherOrderCommand(Guid clientId, string voucherCode)
        {
            ClientId = clientId;
            VoucherCode = voucherCode;
        }

        public Guid ClientId { get; private set; }
        public string VoucherCode { get; private set; }

        public override bool IsValid()
        {
            ValidationResult = new ApplyVoucherOrderValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
