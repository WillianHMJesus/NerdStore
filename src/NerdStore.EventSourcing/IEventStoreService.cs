using EventStore.ClientAPI;

namespace NerdStore.EventSourcing
{
    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}
