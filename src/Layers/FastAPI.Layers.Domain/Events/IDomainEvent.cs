﻿namespace FastAPI.Layers.Domain.Events;

/// <summary>
/// Represents a Domain-level event - a simple POCO class, modeling an occurrence in the domain.
/// </summary>
public interface IDomainEvent
{
    /// <summary>
    /// Gets or sets a value indicating whether the event has been marked as Handled.
    /// </summary>
    bool Handled { get; set; }

    /// <summary>
    /// Gets the instant in time when the event has been raised.
    /// </summary>
    DateTime OccuredOn { get; }
}
