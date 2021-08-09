using Microsoft.EntityFrameworkCore;
using NerdStore.Catalog.Data.Contexts;
using NerdStore.Catalog.Domain.Entities;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly CatalogContext _context;

        public ProductRepository(CatalogContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        #region Product
        public void Add(Product product)
        {
            _context.Products.Add(product);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetByCategoryAsync(int categoryCode)
        {
            return await _context.Products.Where(x => x.Category.Code == categoryCode).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _context.Products.FindAsync(id);
        }

        public void Update(Product product)
        {
            _context.Products.Update(product);
        }
        
        #endregion

        #region Category

        public void AddCategory(Category category)
        {
            _context.Categories.Add(category);
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _context.Categories.AsNoTracking().ToListAsync();
        }

        public async Task<Category> GetCategoryByCodeAsync(int categoryCode)
        {
            return await _context.Categories.FirstOrDefaultAsync(x => x.Code == categoryCode);
        }

        public async Task<Category> GetCategoryByIdAsync(Guid categoryId)
        {
            return await _context.Categories.FindAsync(categoryId);
        }

        public void UpdateCategory(Category category)
        {
            _context.Categories.Update(category);
        }

        #endregion

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
