using System;

namespace NerdStore.Payment.Business.DTOs
{
    public class PaymentOrderDTO
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal Amount { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardSecurityCode { get; set; }
    }
}
