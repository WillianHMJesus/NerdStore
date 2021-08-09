using NerdStore.Core.DomainObjects;
using System;

namespace NerdStore.Catalog.Domain.Events
{
    public class ProductBelowStockEvent : DomainEvent
    {
        public ProductBelowStockEvent(Guid aggregateId)
            : base(aggregateId) { }
    }
}
