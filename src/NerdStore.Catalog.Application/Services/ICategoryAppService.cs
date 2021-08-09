using NerdStore.Catalog.Application.DTOs;
using NerdStore.Catalog.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Application.Services
{
    public interface ICategoryAppService : IDisposable
    {
        Task AddAsync(CategoryViewModel categoryViewModel);
        Task<IEnumerable<CategoryDTO>> GetAllAsync();
        Task<CategoryDTO> GetByIdAsync(Guid id);
        Task UpdateAsync(CategoryViewModel categoryViewModel);
    }
}
