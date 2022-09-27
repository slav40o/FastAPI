namespace FastAPI.Layers.Domain.Events.Abstractions;

/// <summary>
/// Represents a handler for a Domain-level event.
/// </summary>
/// <typeparam name="TEvent">Event type.</typeparam>
public interface IDomainEventHandler<TEvent>
    where TEvent : IDomainEvent
{
    /// <summary>
    /// Handles the domain event.
    /// </summary>
    /// <param name="args">Event instance / Event "args".</param>
    /// <returns>A task representing the Handle operation.</returns>
    Task Handle(TEvent args);
}
