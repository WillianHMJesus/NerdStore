using NerdStore.Core.Messages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NerdStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveEvent<TEvent>(TEvent _event) where TEvent : Event;
        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
    }
}
