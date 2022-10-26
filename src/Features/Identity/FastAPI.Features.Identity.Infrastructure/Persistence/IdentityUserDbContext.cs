namespace FastAPI.Features.Identity.Infrastructure.Persistence;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Infrastructure.Persistence.Events;

using MediatR;

using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Identity user db context.
/// </summary>
public class IdentityUserDbContext : IdentityDbContext<User>, IDataProtectionKeyContext, IEventDbContext
{
    private readonly IMediator eventDispatcher;

    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityUserDbContext"/> class.
    /// </summary>
    /// <param name="options">DB context options.</param>
    /// <param name="eventDispatcher">Event dispatcher.</param>
    public IdentityUserDbContext(DbContextOptions<IdentityUserDbContext> options, IMediator eventDispatcher)
        : base(options)
    {
        this.eventDispatcher = eventDispatcher;
    }

    /// <summary>
    /// Gets or sets data protection keys db set.
    /// </summary>
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; } = default!;

    /// <inheritdoc/>
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await this.DispatchEvents(this.eventDispatcher);
        return await base.SaveChangesAsync(cancellationToken);
    }

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityUserDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
