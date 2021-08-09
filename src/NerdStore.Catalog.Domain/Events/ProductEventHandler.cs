using MediatR;
using NerdStore.Catalog.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductEventHandler : INotificationHandler<ProductBelowStockEvent>
    {
        private readonly IProductRepository _productRepository;

        public ProductEventHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task Handle(ProductBelowStockEvent message, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(message.AggregateId);

            //Enviar produto para o módulo de compras.
        }
    }
}
