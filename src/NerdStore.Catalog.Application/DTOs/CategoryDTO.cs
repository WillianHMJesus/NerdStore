using System;

namespace NerdStore.Catalog.Application.DTOs
{
    public class CategoryDTO
    {
        public Guid Id { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
