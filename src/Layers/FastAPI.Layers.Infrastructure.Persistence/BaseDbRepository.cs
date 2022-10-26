namespace FastAPI.Layers.Infrastructure.Persistence;

using System;
using System.Linq;
using System.Threading.Tasks;

using FastAPI.Layers.Domain.Entities.Abstractions;

using Microsoft.EntityFrameworkCore;

/// <summary>
/// Base repository class.
/// </summary>
/// <typeparam name="TDbContext">type of the db context.</typeparam>
/// <typeparam name="TEntity">type of the entity.</typeparam>
/// <typeparam name="TKey">type of the entity key.</typeparam>
public abstract class BaseDbRepository<TDbContext, TEntity, TKey> : IDbRepository<TDbContext, TEntity, TKey>
    where TEntity : class, IEntity<TKey>, IAggregateRoot
    where TDbContext : DbContext
{
    /// <summary>
    /// Initializes instance of <see cref="BaseRepository"/>.
    /// </summary>
    /// <param name="dbContext">db context.</param>
    protected BaseDbRepository(TDbContext dbContext)
    {
        DbContext = dbContext;
    }

    /// <summary>
    /// Gets Db context.
    /// </summary>
    protected TDbContext DbContext { get; }

    /// <inheritdoc/>
    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        await DbContext.AddAsync(entity, cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> All()
    {
        return DbContext.Set<TEntity>();
    }

    /// <inheritdoc/>
    public async Task<TEntity?> GetAsync(TKey key, CancellationToken cancellationToken = default)
    {
        if (key is null)
        {
            throw new ArgumentNullException(nameof(key));
        }

        return await DbContext.FindAsync<TEntity>(keyValues: new object[] { key }, cancellationToken: cancellationToken);
    }

    /// <inheritdoc/>
    public async Task SaveAsync(CancellationToken cancellationToken = default)
    {
        await DbContext.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    public IQueryable<TEntity> AllAsNoTracking()
    {
        return All().AsNoTracking();
    }
}
