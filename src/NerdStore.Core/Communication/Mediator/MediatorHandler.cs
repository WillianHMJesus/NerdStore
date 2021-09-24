using MediatR;
using NerdStore.Core.Data.EventSourcing;
using NerdStore.Core.Messages;
using NerdStore.Core.Messages.CommonMessages.DomainEvents;
using NerdStore.Core.Messages.CommonMessages.Notifications;
using System.Threading.Tasks;

namespace NerdStore.Core.Communication.Mediator
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator _mediator;
        private readonly IEventSourcingRepository _eventSourcingRepository;

        public MediatorHandler(IMediator mediator,
            IEventSourcingRepository eventSourcingRepository)
        {
            _mediator = mediator;
            _eventSourcingRepository = eventSourcingRepository;
        }

        public async Task PublishDomainEventAsync<T>(T _event) where T : DomainEvent
        {
            await _mediator.Publish(_event);
        }

        public async Task PublishEventAsync<T>(T _event) where T : Event
        {
            await _mediator.Publish(_event);
            await _eventSourcingRepository.SaveEvent(_event);
        }

        public async Task PublishNotificationAsync<T>(T notification) where T : DomainNotification
        {
            await _mediator.Publish(notification);
        }

        public async Task<bool> SendCommandAsync<T>(T command) where T : Command
        {
            return await _mediator.Send(command);
        }
    }
}
