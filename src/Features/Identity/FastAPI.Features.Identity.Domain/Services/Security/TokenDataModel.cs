namespace FastAPI.Features.Identity.Domain.Services.Security;

/// <summary>
/// Token model.
/// </summary>
public record TokenDataModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TokenDataModel"/> class.
    /// </summary>
    internal TokenDataModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TokenDataModel"/> class.
    /// </summary>
    /// <param name="token">Token value.</param>
    /// <param name="expirationTime">Expiration date of the token.</param>
    public TokenDataModel(string token, DateTime expirationTime)
    {
        Value = token;
        ExpirationTime = expirationTime;
    }

    /// <summary>
    /// Gets token value.
    /// </summary>
    public string Value { get; init; } = default!;

    /// <summary>
    /// Gets token date of expiration.
    /// </summary>
    public DateTime ExpirationTime { get; init; }
}
