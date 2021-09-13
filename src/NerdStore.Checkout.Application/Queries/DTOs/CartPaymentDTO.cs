namespace NerdStore.Checkout.Application.Queries.DTOs
{
    public class CartPaymentDTO
    {
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiration { get; set; }
        public string CardSecurityCode { get; set; }
    }
}
