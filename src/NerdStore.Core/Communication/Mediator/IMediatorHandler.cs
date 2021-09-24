using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.DomainEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace NerdStore.Core.Communication.Mediator
{
    public interface IMediatorHandler
    {
        Task PublishDomainEventAsync<T>(T _event) where T : DomainEvent;
        Task PublishEventAsync<T>(T _event) where T : Event;
        Task PublishNotificationAsync<T>(T notification) where T : DomainNotification;
        Task<bool> SendCommandAsync<T>(T command) where T : Command;
    }
}
