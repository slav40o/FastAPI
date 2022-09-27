namespace FastAPI.Features.Identity.Domain.Entities;

using FastAPI.Layers.Domain.Entities.ValueObjects;

/// <summary>
/// Holds data for token value and expiration.
/// </summary>
public sealed record IdentityToken : ValueObject
{
    /// <summary>
    /// Initializes a new instance of the <see cref="IdentityToken"/> class.
    /// </summary>
    /// <param name="value">Token value.</param>
    /// <param name="expirationTime">Token expiration time.</param>
    public IdentityToken(string value, DateTimeOffset expirationTime)
    {
        this.Value = value;
        this.ExpirationTime = expirationTime;
    }

    /// <summary>
    /// Gets the token value.
    /// </summary>
    public string Value { get; init; } = default!;

    /// <summary>
    /// Gets token expiration time.
    /// </summary>
    public DateTimeOffset ExpirationTime { get; init; }
}
