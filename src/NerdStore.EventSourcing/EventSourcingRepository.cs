using EventStore.ClientAPI;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId)
        {
            var events = await _eventStoreService.GetConnection()
                .ReadStreamEventsBackwardAsync(aggregateId.ToString(), 0, 500, false);

            var storedEvents = new List<StoredEvent>();
            foreach (var _event in events.Events)
            {
                var dataEncoded = Encoding.UTF8.GetString(_event.Event.Data);
                var dataJson = JsonConvert.DeserializeObject<BaseEvent>(dataEncoded);
                var storedEvent = new StoredEvent(
                    _event.Event.EventId,
                    _event.Event.EventType,
                    dataJson.Timestamp,
                    dataEncoded);

                storedEvents.Add(storedEvent);
            }

            return storedEvents;
        }

        public async Task SaveEvent<TEvent>(TEvent _event) where TEvent : Event
        {
            await _eventStoreService.GetConnection().AppendToStreamAsync(
                _event.AggregateId.ToString(),
                ExpectedVersion.Any,
                FormatEvent(_event));
        }

        private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent _event) where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                _event.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(_event)),
                null);
        }
    }

    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}
