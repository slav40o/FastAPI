namespace FastAPI.Features.Identity.Domain.Builders.Abstractions;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Layers.Domain.Builders;

/// <summary>
/// Builder for <see cref="User"/>.
/// </summary>
public interface IUserBuilder : IBuilder<User>
{
    /// <summary>
    /// Set user email.
    /// </summary>
    /// <param name="email">Email value.</param>
    /// <returns>Current <see cref="IBuilder{User}"/> instance.</returns>
    IUserBuilder WithEmail(string email);

    /// <summary>
    /// Set user first name.
    /// </summary>
    /// <param name="name">First name value.</param>
    /// <returns>Current <see cref="IBuilder{User}"/> instance.</returns>
    IUserBuilder WithFirstName(string name);

    /// <summary>
    /// Set user last name.
    /// </summary>
    /// <param name="name">Last name value.</param>
    /// <returns>Current <see cref="IBuilder{User}"/> instance.</returns>
    IUserBuilder WithLastName(string name);
}
