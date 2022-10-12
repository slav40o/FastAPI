namespace FastAPI.Layers.Infrastructure.Persistence.SQL;

using System;

/// <summary>
/// Db initializer interface.
/// </summary>
public interface IDbInitializer
{
    /// <summary>
    /// Initializes the db initializer.
    /// </summary>
    Task Initialize();
}
