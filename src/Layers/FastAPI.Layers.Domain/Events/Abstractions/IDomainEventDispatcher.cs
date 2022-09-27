﻿namespace FastAPI.Layers.Domain.Events.Abstractions;

/// <summary>
/// Represents a domain-level event dispatcher.
/// </summary>
public interface IDomainEventDispatcher
{
    /// <summary>
    /// Dispatches the provided domain-level event.
    /// </summary>
    /// <param name="domainEvent">Event instance.</param>
    /// <<returns>A task representing the dispatch operation.</returns>
    Task Dispatch(IDomainEvent domainEvent);
}
