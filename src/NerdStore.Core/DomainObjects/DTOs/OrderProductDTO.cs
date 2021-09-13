using System;

namespace NerdStore.Core.DomainObjects.DTOs
{
    public class OrderProductDTO
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
    }
}
