namespace FastAPI.Features.Identity.Domain.Services.Security;

using System.Security.Claims;
using FastAPI.Features.Identity.Domain.Entities;

/// <summary>
/// Token generator service used for generating authentication tokens.
/// </summary>
public interface IAuthTokenProvider
{
    /// <summary>
    /// Generates authentication token.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="roles">User roles.</param>
    /// <param name="additionalClaims">User claims different from user details data(Email, FirstName, etc.) or roles.</param>
    /// <returns>Generated token model.</returns>
    TokenDataModel GenerateAuthToken(User user, IEnumerable<string> roles, IEnumerable<Claim> additionalClaims, SecurityTokenOptions options);

    /// <summary>
    /// Generate refresh token.
    /// </summary>
    /// <returns>Generated token model.</returns>
    TokenDataModel GenerateRefreshToken(SecurityTokenOptions options);

    /// <summary>
    /// Get principal instance for given authentication token.
    /// </summary>
    /// <param name="token">Authentication token value.</param>
    /// <returns>Generated principal instance.</returns>
    ClaimsPrincipal GetPrincipalFromAuthToken(string token);
}
