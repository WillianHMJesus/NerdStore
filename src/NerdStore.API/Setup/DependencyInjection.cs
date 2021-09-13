using MediatR;
using Microsoft.Extensions.DependencyInjection;
using NerdStore.Catalog.Application.Services;
using NerdStore.Catalog.Data.Contexts;
using NerdStore.Catalog.Data.Repositories;
using NerdStore.Catalog.Domain.Events;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Catalog.Domain.Services;
using NerdStore.Checkout.Application.Commands;
using NerdStore.Checkout.Application.Events;
using NerdStore.Checkout.Application.Queries;
using NerdStore.Checkout.Data.Contexts;
using NerdStore.Checkout.Data.Repositories;
using NerdStore.Checkout.Domain.Interfaces;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using NerdStore.Payment.AntiCorruption;
using NerdStore.Payment.Business.Events;
using NerdStore.Payment.Business.Interfaces;
using NerdStore.Payment.Business.Services;
using NerdStore.Payment.Data.Contexts;
using NerdStore.Payment.Data.Repositories;

namespace NerdStore.API.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // CORE
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<INotificationHandler<DomainNotification>, DomainNotificationHandler>();

            // Catalog
            services.AddScoped<INotificationHandler<ProductBelowStockEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<StartOrderEvent>, ProductEventHandler>();
            services.AddScoped<INotificationHandler<CancelOrderEvent>, ProductEventHandler>();
            
            services.AddScoped<IStockService, StockService>();
            services.AddScoped<IProductAppService, ProductAppService>();
            services.AddScoped<ICategoryAppService, CategoryAppService>();

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();

            // Checkout
            services.AddScoped<IRequestHandler<AddOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateOrderItemCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<ApplyVoucherOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<StartOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<FinishOrderCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderReverseStockCommand, bool>, OrderCommandHandler>();
            services.AddScoped<IRequestHandler<CancelOrderCommand, bool>, OrderCommandHandler>();

            services.AddScoped<INotificationHandler<DraftOrderStartedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderUpdatedOrderEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderItemAddedEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderUpdatedProductEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderRemovedProductEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<VoucherAppliedOrderEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<OrderRejectedStockEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<RealizedPaymentEvent>, OrderEventHandler>();
            services.AddScoped<INotificationHandler<RejectedPaymentEvent>, OrderEventHandler>();

            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderQueries, OrderQueries>();
            services.AddScoped<CheckoutContext>();

            // Payment
            services.AddScoped<INotificationHandler<OrderConfirmedStockEvent>, PaymentEventHandler>();
            
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<ICreditCardPaymentFacade, CreditCardPaymentFacade>();
            services.AddScoped<IPayPalGateway, PayPalGateway>();
            services.AddScoped<IConfigurationManager, ConfigurationManager>();

            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<PaymentContext>();
        }
    }
}
