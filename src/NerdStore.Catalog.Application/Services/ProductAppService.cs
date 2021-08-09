using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.Services
{
    public class ProductAppService : IProductAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMapper _mapper;

        public ProductAppService(IProductRepository productRepository,
            IStockService stockService,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mapper = mapper;
        }

        public async Task AddAsync(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            var category = await _productRepository.GetCategoryByCodeAsync(productViewModel.CategoryCode.Value);
            product.AssignCategory(category);
            _productRepository.Add(product);
            await _productRepository.UnitOfWork.CommitAsync();
        }

        public async Task<ProductDTO> DebitStockAsync(Guid productId, int quantity)
        {
            if (!_stockService.DebitStockAsync(productId, quantity).Result)
            {
                throw new DomainException("Não foi possível debitar o estoque do produto.");
            }

            var product = await _productRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<IEnumerable<ProductDTO>> GetAllAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<IEnumerable<ProductDTO>> GetByCategoryAsync(int categoryCode)
        {
            var products = await _productRepository.GetByCategoryAsync(categoryCode);
            return _mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task<ProductDTO> ReplacingStockAsync(Guid productId, int quantity)
        {
            if (!_stockService.ReplacingStockAsync(productId, quantity).Result)
            {
                throw new DomainException("Não foi possível repor o estoque do produto.");
            }

            var product = await _productRepository.GetByIdAsync(productId);
            return _mapper.Map<ProductDTO>(product);
        }

        public async Task UpdateAsync(ProductViewModel productViewModel)
        {
            var product = _mapper.Map<Product>(productViewModel);
            var category = await _productRepository.GetCategoryByCodeAsync(productViewModel.CategoryCode.Value);
            product.AssignCategory(category);
            _productRepository.Update(product);
            await _productRepository.UnitOfWork.CommitAsync();
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
            _stockService?.Dispose();
        }
    }
}
