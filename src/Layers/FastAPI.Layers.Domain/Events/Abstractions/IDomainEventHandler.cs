namespace FastAPI.Layers.Domain.Events.Abstractions;

using Mediator;

/// <summary>
/// Represents a handler for a Domain-level event.
/// </summary>
/// <typeparam name="TEvent">Event type.</typeparam>
public interface IDomainEventHandler<TEvent> : INotificationHandler<TEvent>
    where TEvent : IDomainEvent
{
}
