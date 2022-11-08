namespace FastAPI.Layers.Infrastructure.Persistence;

using Events;

using Mediator;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;

/// <summary>
/// Base DB context.
/// </summary>
/// <typeparam name="TContext">Implementing context type.</typeparam>
public abstract class BaseDbContext<TContext> : DbContext, IEventDbContext
    where TContext : DbContext
{
    private readonly IMediator dispatcher;

    /// <summary>
    /// Initializes a new instance of the <see cref="TContext"/> class.
    /// </summary>
    /// <param name="options">DB context options.</param>
    /// <param name="dispatcher">Event dispatching.</param>
    public BaseDbContext(DbContextOptions<TContext> options, IMediator dispatcher)
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
         => AsyncHelper.RunSync(() => SaveChangesAsync(cancellationToken));

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await this.DispatchEvents(dispatcher);
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TContext).Assembly);
    }
}
