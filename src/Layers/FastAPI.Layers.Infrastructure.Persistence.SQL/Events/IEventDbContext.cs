namespace FastAPI.Layers.Infrastructure.Persistence.SQL.Events;

using Microsoft.EntityFrameworkCore.ChangeTracking;

/// <summary>
/// Interface for db context that can fire events.
/// </summary>
public interface IEventDbContext
{
    /// <summary>
    /// Gets db context change tracked.
    /// </summary>
    ChangeTracker ChangeTracker { get; }
}
