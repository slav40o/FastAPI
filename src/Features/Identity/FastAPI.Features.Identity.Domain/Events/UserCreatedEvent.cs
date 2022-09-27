namespace FastAPI.Features.Identity.Domain.Events;

using FastAPI.Layers.Domain.Events;

public sealed class UserCreatedEvent : DomainEvent
{
    public string Id { get; init; } = default!;

    public string Email { get; init; } = default!;

    public string FirstName { get; init; } = default!;

    public string LastName { get; init; } = default!;
}
