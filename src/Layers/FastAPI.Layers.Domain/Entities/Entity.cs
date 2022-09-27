namespace FastAPI.Layers.Domain.Entities;

using Abstractions;
using Events;
using FastAPI.Layers.Domain.Events.Abstractions;

/// <summary>
/// Base entity class.
/// </summary>
/// <typeparam name="T">type of base class id.</typeparam>
public abstract class Entity<T> : IEntity<T>
{
    private ICollection<IDomainEvent> events;

    /// <summary>
    /// Initializes a new instance of the <see cref="Entity{T}"/> class.
    /// </summary>
    protected Entity()
    {
        this.Id = default!;
        this.events = new List<IDomainEvent>();
    }

    /// <summary>
    /// Gets id of the entity.
    /// </summary>
    public virtual T Id { get; private set; }

    /// <inheritdoc/>
    public IReadOnlyCollection<IDomainEvent> Events => this.events.ToArray();

    public static bool operator ==(Entity<T>? left, Entity<T>? right)
        => EqualityComparer<Entity<T>>.Default.Equals(left, right);

    public static bool operator !=(Entity<T>? left, Entity<T>? right)
        => !(left == right);

    /// <inheritdoc/>
    public void AddEvent(IDomainEvent domainEvent)
        => this.events.Add(domainEvent);

    /// <inheritdoc/>
    public void ClearEvents()
        => this.events = new List<IDomainEvent>();

    /// <summary>
    /// Indicates whether the entity is in transient state - e.g. not persistent.
    /// </summary>
    /// <returns>True if object is transient, otherwise false.</returns>
    public bool IsTransient()
    {
        if (this.Id is null)
        {
            return true;
        }

        return this.Id.Equals(default(T));
    }

    /// <summary>
    /// Indicates whether two objects are equal.
    /// </summary>
    /// <param name="obj">Object to test equality.</param>
    /// <returns>True if objects are equal, otherwise false.</returns>
    public override bool Equals(object? obj)
    {
        // TODO extend equals checks
        return obj is Entity<T> entity &&
               EqualityComparer<T>.Default.Equals(this.Id, entity.Id);
    }

    /// <inheritdoc/>
    public override int GetHashCode()
    {
        if (this.IsTransient())
        {
            return base.GetHashCode();
        }

        return HashCode.Combine(this.Id);
    }
}
