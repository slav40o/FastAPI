namespace FastAPI.Layers.Application.Services;

using System.Security.Claims;

public interface ICurrentUser
{
    /// <summary>
    /// Gets current user id.
    /// </summary>
    string UserId { get; }

    /// <summary>
    /// Gets current user name.
    /// </summary>
    string UserName { get; }

    /// <summary>
    /// Gets current user first name.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// Gets current user last name.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Gets a value indicating whether user has admin role.
    /// </summary>
    bool IsAdmin { get; }

    /// <summary>
    /// Gets current user email.
    /// </summary>
    string Email { get; }

    /// <summary>
    /// Check if current user matches given claim condition.
    /// </summary>
    /// <param name="action">Claim matching action.</param>
    /// <returns>Whether current user has any matching claim to the given condition.</returns>
    bool Claims(Func<Claim, bool> action);
}
