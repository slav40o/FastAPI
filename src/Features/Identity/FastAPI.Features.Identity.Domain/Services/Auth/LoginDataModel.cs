using FastAPI.Features.Identity.Domain.Services.Security;

namespace FastAPI.Features.Identity.Domain.Services.Auth;

/// <summary>
/// Login response model
/// </summary>
public record LoginDataModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoginDataModel"/> class.
    /// </summary>
    internal LoginDataModel()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginDataModel"/> class.
    /// </summary>
    /// <param name="authToken">Authentication token.</param>
    /// <param name="refreshToken">Refresh token.</param>
    public LoginDataModel(TokenDataModel authToken, TokenDataModel refreshToken)
    {
        this.AuthToken = authToken;
        this.RefreshToken = refreshToken;
    }

    /// <summary>
    /// Gets authentication token.
    /// </summary>
    public TokenDataModel AuthToken { get; init; } = default!;

    /// <summary>
    /// Gets refresh token.
    /// </summary>
    public TokenDataModel RefreshToken { get; init; } = default!;
}
