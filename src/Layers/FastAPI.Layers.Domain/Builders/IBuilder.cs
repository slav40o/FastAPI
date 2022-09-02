namespace FastAPI.Layers.Domain.Builders;

using Entities.Abstractions;

/// <summary>
/// Main builder interface for creating root domain entities.
/// </summary>
/// <typeparam name="TEntity">Type of the aggregate entity.</typeparam>
public interface IBuilder<out TEntity>
    where TEntity : IAggregateRoot
{
    /// <summary>
    /// Builds new instance of the <typeparamref name="TEntity"/> class.
    /// </summary>
    /// <returns>Returns the created instance.</returns>
    TEntity Build();
}
