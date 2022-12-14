namespace FastAPI.Layers.Infrastructure.Persistence.Events;

using FastAPI.Layers.Domain.Entities.Abstractions;

using Mediator;

/// <summary>
/// Db context extensions.
/// </summary>
public static class DbContextEventsExtensions
{
    /// <summary>
    /// Dispatch domain events of all entities that are modified.
    /// </summary>
    /// <param name="context">Db Context.</param>
    /// <param name="dispatcher">Event dispatcher.</param>
    /// <returns>Performed task.</returns>
    public static async Task DispatchEvents(this IEventDbContext context, IMediator dispatcher)
    {
        var entities = context.ChangeTracker
            .Entries<IBaseEntity>()
            .Select(e => e.Entity)
            .Where(e => e.Events.Any())
            .ToArray();

        foreach (var entity in entities)
        {
            var events = entity.Events.ToArray();

            foreach (var domainEvent in events)
            {
                await dispatcher.Publish(domainEvent);
            }

            entity.ClearEvents();
        }
    }
}
