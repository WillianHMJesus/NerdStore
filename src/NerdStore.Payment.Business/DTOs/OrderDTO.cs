using System;
using System.Collections.Generic;

namespace NerdStore.Payment.Business.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public decimal Value { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
