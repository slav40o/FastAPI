namespace FastAPI.Layers.Domain.Entities.Abstractions;

using ValueObjects;

/// <summary>
/// Entity that holds basic data for its creator and last modifier.
/// </summary>
public interface IAuditableEntity
{
    /// <summary>
    /// Gets creator data.
    /// </summary>
    ModificationDetails CreationDetails { get; }

    /// <summary>
    /// Gets creator data.
    /// </summary>
    ModificationDetails? LastModificationDetails { get; }
}
