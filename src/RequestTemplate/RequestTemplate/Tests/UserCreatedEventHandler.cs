namespace RequestTemplate.Tests;

using FastAPI.Layers.Application;
using FastAPI.Layers.Domain.Events.Abstractions;

using System.Threading;
using System.Threading.Tasks;

public sealed class UserCreatedEventHandlerHandler : IDomainEventHandler<UserCreatedEvent>
{
    public UserCreatedEventHandlerHandler()
    {
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
    }
}