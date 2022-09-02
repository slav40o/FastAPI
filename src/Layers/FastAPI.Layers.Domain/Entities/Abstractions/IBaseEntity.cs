namespace FastAPI.Layers.Domain.Entities.Abstractions;

using Events;

/// <summary>
/// Entity base.
/// </summary>
public interface IBaseEntity
{
    /// <summary>
    /// Gets a readonly collection of the attached domain-level events.
    /// </summary>
    IReadOnlyCollection<IDomainEvent> Events { get; }

    /// <summary>
    /// Attaches a domain-level event to be raised on the current entity.
    /// </summary>
    /// <param name="domainEvent">Domain event instance.</param>
    void AddEvent(IDomainEvent domainEvent);

    /// <summary>
    /// Clears all domain-level event handlers attached to the current entity.
    /// </summary>
    void ClearEvents();
}
