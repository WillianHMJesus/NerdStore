using System;

namespace NerdStore.Catalog.Application.DTOs
{
    public class ProductDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string Image { get; set; }
        public int Quantity { get; set; }
        public DateTime Register { get; set; }
        public bool Active { get; set; }
        public decimal Height { get; set; }
        public decimal Width { get; set; }
        public decimal Depth { get; set; }
        public CategoryDTO Category { get; set; }
    }
}
