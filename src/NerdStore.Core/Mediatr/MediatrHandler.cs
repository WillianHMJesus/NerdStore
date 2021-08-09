using MediatR;
using NerdStore.Core.Messages;
using System.Threading.Tasks;

namespace NerdStore.Core.Mediatr
{
    public class MediatrHandler : IMediatrHandler
    {
        private readonly IMediator _mediator;

        public MediatrHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task PublishEventAsync<T>(T _event) where T : Event
        {
            await _mediator.Publish(_event);
        }
    }
}
