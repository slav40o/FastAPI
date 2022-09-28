namespace FastAPI.Features.Identity.Domain.Repositories;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Domain.Repositories;


/// <summary>
/// Repository for <see cref="User"/>.
/// </summary>
public interface IUserRepository : IDomainRepository<User, string>
{
    /// <summary>
    /// Find user by email async.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task with fetched user instance.</returns>
    Task<User?> FindByEmailAsync(string email, CancellationToken cancellationToken = default);

    /// <summary>
    /// Find user by Id async.
    /// </summary>
    /// <param name="id">User id.</param>
    /// <param name="cancellationToken">Cancellation Token.</param>
    /// <returns>Task with fetched user instance.</returns>
    Task<User?> FindByIdAsync(string id, CancellationToken cancellationToken = default);
}
