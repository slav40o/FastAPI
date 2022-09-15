namespace FastAPI.Layers.Domain.Builders;

using Entities.Abstractions;

/// <summary>
/// Extended builder with additional options data that is required for the creation of the entity.
/// For example creator user data.
/// </summary>
/// <typeparam name="TEntity">Type of the aggregate entity.</typeparam>
public interface IAuditableEntityBuilder<out TEntity> : IBuilder<TEntity>
    where TEntity : IAggregateRoot, IAuditableEntity
{
    /// <summary>
    /// Provides additional options for the creation of entity.
    /// </summary>
    /// <param name="auditor">Options that provide additional data for the created entity.</param>
    /// <returns>Current instance.</returns>
    IAuditableEntityBuilder<TEntity> WithAuditor(IModifierDetails auditor);
}
