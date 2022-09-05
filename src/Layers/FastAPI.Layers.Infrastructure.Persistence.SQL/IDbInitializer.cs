namespace FastAPI.Layers.Persistence;

using System;

/// <summary>
/// Db initializer interface.
/// </summary>
public interface IDbInitializer
{
    /// <summary>
    /// Initializes the db initializer.
    /// </summary>
    void Initialize();
}
