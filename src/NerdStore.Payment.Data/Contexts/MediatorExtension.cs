using NerdStore.Core.Communication.Mediator;
using NerdStore.Core.DomainObjects;
using System.Linq;
using System.Threading.Tasks;

namespace NerdStore.Payment.Data.Contexts
{
    public static class MediatorExtension
    {
        public static async Task PublishEventsAsync(this IMediatorHandler mediator, PaymentContext context)
        {
            var entities = context.ChangeTracker.Entries<Entity>()
                .Where(x => x.Entity.Events != null && x.Entity.Events.Any());

            var events = entities
                .SelectMany(x => x.Entity.Events).ToList();

            entities.ToList().ForEach(x => x.Entity.ClearEvents());

            var tasks = events.Select(async (_event) =>
            {
                await mediator.PublishEventAsync(_event);
            });

            await Task.WhenAll(tasks);
        }
    }
}
