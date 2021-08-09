using NerdStore.Catalog.Domain.Entities;
using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        #region Product
        void Add(Product product);
        Task<IEnumerable<Product>> GetAllAsync();
        Task<IEnumerable<Product>> GetByCategoryAsync(int categoryCode);
        Task<Product> GetByIdAsync(Guid id);
        void Update(Product product);
        #endregion

        #region Category
        void AddCategory(Category category);
        Task<IEnumerable<Category>> GetAllCategoriesAsync();
        Task<Category> GetCategoryByCodeAsync(int categoryCode);
        Task<Category> GetCategoryByIdAsync(Guid categoryId);
        void UpdateCategory(Category category);
        #endregion
    }
}
