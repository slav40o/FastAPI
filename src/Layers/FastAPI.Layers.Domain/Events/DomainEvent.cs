namespace FastAPI.Layers.Domain.Events;

using FastAPI.Layers.Domain.Events.Abstractions;

public abstract class DomainEvent : IDomainEvent
{
    public bool Handled { get; set; }

    public DateTime OccuredOn { get; init; }
}
