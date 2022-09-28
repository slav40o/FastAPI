namespace FastAPI.Features.Identity.Domain.Services.Security;

using System.Security.Cryptography;

/// <summary>
/// Random token provider based on <see cref="RandomNumberGenerator"/>.
/// </summary>
public sealed class RandomTokenProvider : IRandomTokenProvider
{
    /// <inheritdoc/>
    public string GenerateRandomToken(int byteArrayLengtht = 64)
    {
        using var random = RandomNumberGenerator.Create();
        byte[] randomNumberBytes = new byte[byteArrayLengtht];
        random.GetBytes(randomNumberBytes);

        string tokenValue = Convert.ToBase64String(randomNumberBytes);

        return tokenValue;
    }
}
