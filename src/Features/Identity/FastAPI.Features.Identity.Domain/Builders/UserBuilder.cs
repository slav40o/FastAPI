namespace FastAPI.Features.Identity.Domain.Builders;

using FastAPI.Features.Identity.Domain.Builders.Abstractions;
using FastAPI.Features.Identity.Domain.Entities;

/// <inheritdoc />
public sealed class UserBuilder : IUserBuilder
{
    private string email = default!;
    private string firstName = default!;
    private string lastName = default!;

    /// <inheritdoc />
    public User Build()
        => new User(this.email, this.firstName, this.lastName);

    /// <inheritdoc />
    public IUserBuilder WithEmail(string email)
    {
        this.email = email;
        return this;
    }

    /// <inheritdoc />
    public IUserBuilder WithFirstName(string name)
    {
        this.firstName = name;
        return this;
    }

    /// <inheritdoc />
    public IUserBuilder WithLastName(string name)
    {
        this.lastName = name;
        return this;
    }
}
