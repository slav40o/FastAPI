namespace FastAPI.Features.Identity.Domain.Services.Security;

/// <summary>
/// Random token provider service.
/// </summary>
public interface IRandomTokenProvider
{
    /// <summary>
    /// Generates random security token. It is not based on salt and it is pure random generated string.
    /// </summary>
    /// <param name="bufferLength">How large should be the byte array used for the token generator.</param>
    /// <returns>Generated token string.</returns>
    string GenerateRandomToken(int bufferLength = 64);
}
