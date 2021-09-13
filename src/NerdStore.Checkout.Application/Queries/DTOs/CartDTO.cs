using System;
using System.Collections.Generic;

namespace NerdStore.Checkout.Application.Queries.DTOs
{
    public class CartDTO
    {
        public Guid OrderId { get; set; }
        public Guid ClientId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Amount { get; set; }
        public decimal DiscountValue { get; set; }
        public string VoucherCode { get; set; }
        public CartPaymentDTO Payment { get; set; }
        public List<CartItemDTO> Items { get; set; } = new List<CartItemDTO>();
    }
}
