using AutoMapper;
using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Application.ViewModels;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.DomainObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.Services
{
    public class CategoryAppService : ICategoryAppService
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public CategoryAppService(IProductRepository productRepository,
            IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            AssertionConcern.ValidateFalse(
                ValidateDuplicationRules(category), "Código da categoria já está sendo utilizado.");

            _productRepository.AddCategory(category);
            await _productRepository.UnitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<CategoryDTO>> GetAllAsync()
        {
            var categories = await _productRepository.GetAllCategoriesAsync();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> GetByIdAsync(Guid id)
        {
            var category = await _productRepository.GetCategoryByIdAsync(id);
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task UpdateAsync(CategoryViewModel categoryViewModel)
        {
            var category = _mapper.Map<Category>(categoryViewModel);
            AssertionConcern.ValidateFalse(
                ValidateDuplicationRules(category), "Código da categoria já está sendo utilizado.");

            _productRepository.UpdateCategory(category);
            await _productRepository.UnitOfWork.CommitAsync();
        }

        private bool ValidateDuplicationRules(Category category)
        {
            var result = _productRepository.GetAllCategoriesAsync().Result
                .FirstOrDefault(x => x.Code == category.Code);

            if (category.Equals(result)) return true;

            return result == null;
        }

        public void Dispose()
        {
            _productRepository?.Dispose();
        }
    }
}
