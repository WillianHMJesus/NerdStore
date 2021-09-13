using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects;
using NerdStore.Core.DomainObjects.DTOs;
using System;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Services
{
    public class StockService : IStockService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMediatorHandler _mediatrHandler;

        public StockService(IProductRepository productRepository,
            IMediatorHandler mediatrHandler)
        {
            _productRepository = productRepository;
            _mediatrHandler = mediatrHandler;
        }

        #region Debit
        public async Task<bool> DebitStockAsync(Guid productId, int quantity)
        {
            await DebitStockItemAsync(productId, quantity);
            return await _productRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> DebitStockProductListAsync(OrderProductListDTO orderProductList)
        {
            foreach (var item in orderProductList.Items)
            {
                await DebitStockItemAsync(item.Id, item.Quantity);
            }

            return await _productRepository.UnitOfWork.CommitAsync();
        }

        private async Task DebitStockItemAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            ValidateProduct(product);
            product.DebitStock(quantity);
            _productRepository.Update(product);

            if (product.Quantity == 0)
            {
                await _mediatrHandler.PublishEventAsync(new ProductBelowStockEvent(product.Id));
            }
        }

        private void ValidateProduct(Product product)
        {
            if (product == null)
            {
                throw new DomainException("Não foi possível localizar o produto informado.");
            }
        }
        #endregion

        #region Replacing
        public async Task<bool> ReplacingStockAsync(Guid productId, int quantity)
        {
            await ReplacingStockItemAsync(productId, quantity);
            return await _productRepository.UnitOfWork.CommitAsync();
        }

        public async Task<bool> ReplacingStockProductListAsync(OrderProductListDTO orderProductList)
        {
            foreach (var item in orderProductList.Items)
            {
                await ReplacingStockItemAsync(item.Id, item.Quantity);
            }

            return await _productRepository.UnitOfWork.CommitAsync();
        }

        private async Task ReplacingStockItemAsync(Guid productId, int quantity)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            ValidateProduct(product);
            product.ReplacingStock(quantity);
            _productRepository.Update(product);
        }
        #endregion

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
