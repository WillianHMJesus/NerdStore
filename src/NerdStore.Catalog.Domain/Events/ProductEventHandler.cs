using MediatR;
using NerdStore.Catalog.Domain.Interfaces;
using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.Messages.CommonMessages.IntegrationEvents;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler :
        INotificationHandler<ProductBelowStockEvent>,
        INotificationHandler<StartOrderEvent>,
        INotificationHandler<CancelOrderEvent>
    {
        private readonly IProductRepository _productRepository;
        private readonly IStockService _stockService;
        private readonly IMediatorHandler _mediator;

        public ProductEventHandler(IProductRepository productRepository,
            IStockService stockService, 
            IMediatorHandler mediator)
        {
            _productRepository = productRepository;
            _stockService = stockService;
            _mediator = mediator;
        }

        public async Task Handle(ProductBelowStockEvent message, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(message.AggregateId);

            //Enviar produto para o módulo de compras.
        }

        public async Task Handle(StartOrderEvent message, CancellationToken cancellationToken)
        {
            var result = await _stockService.DebitStockProductListAsync(message.Items);
            if (result)
            {
                await _mediator.PublishEventAsync(new OrderConfirmedStockEvent(message.OrderId, message.ClientId, message.Amount, message.Items, message.CardName, 
                    message.CardNumber, message.CardExpiration, message.CardSecurityCode));
            }
            else
            {
                await _mediator.PublishEventAsync(new OrderRejectedStockEvent(message.OrderId, message.ClientId));
            }
        }

        public async Task Handle(CancelOrderEvent message, CancellationToken cancellationToken)
        {
            await _stockService.ReplacingStockProductListAsync(message.ProductsOrder);
        }
    }
}
