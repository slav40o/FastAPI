namespace $rootnamespace$;

using FastAPI.Layers.Application;
using FastAPI.Layers.Domain.Events.Abstractions;

using System.Threading;
using System.Threading.Tasks;

public sealed class $safeitemname$Handler : IDomainEventHandler<$safeitemname$>
{
    public $safeitemname$Handler()
    {
    }

    public async Task Handle($safeitemname$ notification, CancellationToken cancellationToken)
    {
    }
}