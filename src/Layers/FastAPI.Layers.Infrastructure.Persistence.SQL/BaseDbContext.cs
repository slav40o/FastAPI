namespace FastAPI.Layers.Persistence;

using Microsoft.EntityFrameworkCore;

using FastAPI.Layers.Persistence.Events;
using System.Threading.Tasks;
using MediatR;
using FastAPI.Layers.Domain.Events.Abstractions;

/// <summary>
/// Base DB context.
/// </summary>
/// <typeparam name="TContext">Implementing context type.</typeparam>
public abstract class BaseDbContext<TContext> : DbContext, IEventDbContext
    where TContext : DbContext
{
    private readonly IDomainEventDispatcher dispatcher;

    /// <summary>
    /// Initializes a new instance of the <see cref="TContext"/> class.
    /// </summary>
    /// <param name="options">DB context options.</param>
    /// <param name="dispatcher">Event dispatching.</param>
    public BaseDbContext(DbContextOptions<TContext> options, IDomainEventDispatcher dispatcher)
        : base(options)
    {
        this.dispatcher = dispatcher;
    }

    /// <summary>
    /// Saves all changes made in this context to the database.
    /// </summary>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>A Task that represents the Save operation. The task result contains the number of state entries written to the database.</returns>
    public int SaveChanges(CancellationToken cancellationToken = default)
         => AsyncHelper.RunSync(() => this.SaveChangesAsync(cancellationToken));

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await this.DispatchEvents(this.dispatcher);
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);
    }
}
