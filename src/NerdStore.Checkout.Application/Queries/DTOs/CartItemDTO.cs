using System;

namespace NerdStore.Checkout.Application.Queries.DTOs
{
    public class CartItemDTO
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitValue { get; set; }
        public decimal Amount { get; set; }
    }
}
