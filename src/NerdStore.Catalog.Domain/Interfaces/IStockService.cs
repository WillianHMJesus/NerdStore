using NerdStore.Core.DomainObjects.DTOs;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Interfaces
{
    public interface IStockService : IDisposable
    {
        Task<bool> DebitStockAsync(Guid productId, int quantity);
        Task<bool> DebitStockProductListAsync(OrderProductListDTO orderProductList);
        Task<bool> ReplacingStockAsync(Guid productId, int quantity);
        Task<bool> ReplacingStockProductListAsync(OrderProductListDTO orderProductList);
    }
}
