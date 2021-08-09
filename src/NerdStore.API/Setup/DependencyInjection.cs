using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data.Contexts;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Core.Mediatr;

namespace NerdStore.API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // Core
            services.AddScoped<IMediatrHandler, MediatrHandler>();

            // Catalog
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<ICategoryAppService, CategoryAppService>();
            services.AddScoped<CatalogContext>();
            services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
        }
    }
}
