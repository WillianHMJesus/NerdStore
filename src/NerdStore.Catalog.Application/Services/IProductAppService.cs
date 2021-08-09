using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.Services
{
    public interface IProductAppService : IDisposable
    {
        Task AddAsync(ProductViewModel productViewModel);
        Task<ProductDTO> DebitStockAsync(Guid productId, int quantity);
        Task<IEnumerable<ProductDTO>> GetAllAsync();
        Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int categoryCode);
        Task<ProductDTO> GetByIdAsync(Guid id);
        Task<ProductDTO> ReplacingStockAsync(Guid productId, int quantity);
        Task UpdateAsync(ProductViewModel productViewModel);
    }
}
