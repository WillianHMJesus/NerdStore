using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.Mediatr;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatrHandler _mediatrHandler;

        public StockService(IProductRepository productRepository,
            IMediatrHandler mediatrHandler)
        {
            _productRepository = productRepository;
            _mediatrHandler = mediatrHandler;
        }

        public async Task<bool> DebitStockAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            ValidateProduct(product);
            product.DebitStock(quantity);
            _productRepository.Update(product);

            if(product.Quantity == 0)
            {
                await _mediatrHandler.PublishEventAsync(new ProductBelowStockEvent(product.Id));
            }

            return await _productRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> ReplacingStockAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            ValidateProduct(product);
            product.ReplacingStock(quantity);
            _productRepository.Update(product);

            return await _productRepository.UnitOfWork.CommitAsync();
        }

        private void ValidateProduct(Product product)
        {
            if(product == null)
            {
                throw new DomainException("Não foi possível localizar o produto informado.");
            }
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
