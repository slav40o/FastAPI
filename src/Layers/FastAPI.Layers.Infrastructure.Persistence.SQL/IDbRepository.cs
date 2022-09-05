namespace FastAPI.Layers.Persistence;

using FastAPI.Layers.Domain.Entities.Abstractions;
using FastAPI.Layers.Domain.Repositories;

/// <summary>
/// Base repository interface.
/// </summary>
/// <typeparam name="TDbContext">Db Context type.</typeparam>
/// <typeparam name="TEntity">Entity type.</typeparam>
/// <typeparam name="TKey">Entity key type.</typeparam>
public interface IDbRepository<TDbContext, TEntity, TKey> : IDomainRepository<TEntity, TKey>
    where TEntity : IEntity<TKey>, IAggregateRoot
{
    /// <summary>
    /// All entities of type <see cref="TEntity"/> as no tracking.
    /// </summary>
    /// <returns>Returns a query of all entities of type <see cref="TEntity"/> as no tracking.</returns>
    IQueryable<TEntity> AllAsNoTracking();
}
