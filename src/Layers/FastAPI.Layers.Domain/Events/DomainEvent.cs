namespace FastAPI.Layers.Domain.Events;

using FastAPI.Layers.Domain.Events.Abstractions;

using Mediator;

public abstract class DomainEvent : IDomainEvent, INotification
{
    public DomainEvent()
    {
        this.OccuredOn = DateTimeOffset.UtcNow;
    }

    public bool Handled { get; set; }

    public DateTimeOffset OccuredOn { get; init; }
}
