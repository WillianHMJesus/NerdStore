using NerdStore.Core.Messages;
using System.Threading.Tasks;

namespace NerdStore.Core.Mediatr
{
    public interface IMediatrHandler
    {
        Task PublishEventAsync<T>(T _event) where T : Event;
    }
}
