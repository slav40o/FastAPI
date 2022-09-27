namespace FastAPI.Features.Identity.Domain.Services.Security;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Features.Identity.Domain.Services.Auth;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


/// <inheritdoc />
public class JwtAuthTokenProvider : IAuthTokenProvider
{
    private const string AuthenticationSecretKey = "Authentication:Secret";

    private readonly IConfiguration configuration;
    private readonly IRandomTokenProvider randomTokenProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtAuthTokenProvider"/> class.
    /// </summary>
    /// <param name="configuration">Application configuration.</param>
    /// <param name="randomTokenProvider">Random token provider service.</param>
    /// <param name="identitySettingsOptions">Identity Settings Options.</param>
    public JwtAuthTokenProvider(
        IConfiguration configuration,
        IRandomTokenProvider randomTokenProvider)
    {
        this.configuration = configuration;
        this.randomTokenProvider = randomTokenProvider;
    }

    /// <inheritdoc />
    public TokenDataModel GenerateAuthToken(
        User user, 
        IEnumerable<string> roles,
        IEnumerable<Claim> additionalClaims,
        SecurityTokenOptions options)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Email, user.UserName),
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.GivenName, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
        };

        if (user.EmailConfirmed)
        {
            claims.Add(new Claim(CustomClaimTypes.EmailConfirmed, true.ToString()));
        }

        foreach (string? role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        foreach (var item in additionalClaims)
        {
            claims.Add(item);
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Audience = options.ValidAudience,
            Expires = DateTime.UtcNow.AddMinutes(options.TimeSpanInMinutes),
            IssuedAt = DateTime.UtcNow,
            Issuer = options.ValidIssuer,
            NotBefore = DateTime.UtcNow,
            SigningCredentials = new SigningCredentials(GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256Signature),
            Subject = new ClaimsIdentity(claims),
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);
        string? encryptedToken = tokenHandler.WriteToken(token);

        return new TokenDataModel(encryptedToken, tokenDescriptor.Expires.Value);
    }

    /// <inheritdoc />
    public TokenDataModel GenerateRefreshToken(SecurityTokenOptions options)
    {
        var tokenValue = randomTokenProvider.GenerateRandomToken();
        DateTime tokenExpiration = DateTime.UtcNow.AddMinutes(options.TimeSpanInMinutes);

        return new TokenDataModel(tokenValue, tokenExpiration);
    }

    /// <inheritdoc />
    public ClaimsPrincipal GetPrincipalFromAuthToken(string token)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = GetSymmetricSecurityKey(),
            ValidateLifetime = false,
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
        var jwtSecurityToken = securityToken as JwtSecurityToken;
        bool isHmacSha256 =
            jwtSecurityToken is not null &&
            jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

        if (!isHmacSha256)
        {
            throw new SecurityTokenException("Invalid authentication token");
        }

        return principal;
    }

    private SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        string? secret = configuration[AuthenticationSecretKey];
        byte[] key = Encoding.ASCII.GetBytes(secret);

        return new SymmetricSecurityKey(key);
    }
}
