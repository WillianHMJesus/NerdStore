using System;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Interfaces
{
    public interface IStockService : IDisposable
    {
        Task<bool> DebitStockAsync(Guid productId, int quantity);
        Task<bool> ReplacingStockAsync(Guid productId, int quantity);
    }
}
