using System;
using System.Collections.Generic;

namespace NerdStore.Core.DomainObjects.DTOs
{
    public class OrderProductListDTO
    {
        public Guid OrderId { get; set; }
        public ICollection<OrderProductDTO> Items { get; set; }
    }
}
