namespace FastAPI.Features.Identity.Infrastructure.Persistence.Repositories;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Features.Identity.Domain.Repositories;
using FastAPI.Layers.Infrastructure.Persistence.SQL;

using Microsoft.EntityFrameworkCore;

using System.Threading.Tasks;


/// <inheritdoc />
public sealed class UserRepository : BaseDbRepository<IdentityUserDbContext, User, string>, IUserRepository
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UserRepository"/> class.
    /// </summary>
    /// <param name="dbContext">Identity user db context.</param>
    public UserRepository(IdentityUserDbContext dbContext)
        : base(dbContext)
    {
    }

    /// <inheritdoc />
    public async Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default)
        => await this.DbContext.Users.Where(u => u.Email == email).FirstOrDefaultAsync(cancellationToken);

    /// <inheritdoc />
    public async Task<User?> FindByIdAsync(string id, CancellationToken cancellationToken = default)
        => await this.GetAsync(id, cancellationToken);
}
