namespace FastAPI.Features.Identity.Domain.Events;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Domain.Events;

public sealed class UserCreatedEvent : DomainEvent
{
    public UserCreatedEvent(User user)
    {
        this.Email = user.Email;
        this.FirstName = user.FirstName;
        this.LastName = user.LastName;
        this.Id = user.Id;
    }

    public string Id { get; init; } = default!;

    public string Email { get; init; } = default!;

    public string FirstName { get; init; } = default!;

    public string LastName { get; init; } = default!;
}
