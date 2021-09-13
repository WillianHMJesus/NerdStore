using System;

namespace NerdStore.Checkout.Application.Queries.DTOs
{
    public class OrderDTO
    {
        public int Code { get; set; }
        public decimal Amount { get; set; }
        public DateTime Register { get; set; }
        public int OrderStatus { get; set; }
    }
}
