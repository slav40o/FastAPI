namespace FastAPI.Layers.Domain.Common.User;

/// <summary>
/// General user information interface.
/// </summary>
public interface IUser
{
    /// <summary>
    /// Gets user Id.
    /// </summary>
    string Id { get; }

    /// <summary>
    /// Gets user first name.
    /// </summary>
    string FirstName { get; }

    /// <summary>
    /// Gets user last name.
    /// </summary>
    string LastName { get; }

    /// <summary>
    /// Gets user email.
    /// </summary>
    string Email { get; }
}
