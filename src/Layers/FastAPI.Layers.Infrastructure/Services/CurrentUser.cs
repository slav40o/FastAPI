namespace FastAPI.Layers.Infrastructure.Services;

using FastAPI.Layers.Application.Services;
using FastAPI.Layers.Application.Settings;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

/// <summary>
/// Holds information about the current user performing the request to the server.
/// </summary>
public class CurrentUser : ICurrentUser
{
    private const string NoUserMessage = "This request does not have an authenticated user.";

    private readonly ClaimsPrincipal principal;
    private readonly AppSettings appSettings;

    /// <summary>
    /// Initializes a new instance of the <see cref="CurrentUser"/> class.
    /// </summary>
    /// <param name="context">HTTP context.</param>
    /// <param name="appSettingsOptions">Application settings.</param>
    public CurrentUser(IHttpContextAccessor context, IOptions<AppSettings> appSettingsOptions)
    {
        var user = context.HttpContext?.User;
        if (user is null)
        {
            throw new InvalidOperationException(NoUserMessage);
        }

        if (!user.Claims.Any())
        {
            user = HandleUnauthenticatedUser(context);
        }

        this.principal = user;
        this.appSettings = appSettingsOptions.Value;
    }

    /// <inheritdoc />
    public string UserId =>
        this.principal.FindFirstValue(ClaimTypes.NameIdentifier) ??
        this.principal.FindFirstValue("nameid")!;

    /// <inheritdoc />
    public string UserName =>
        this.principal.FindFirstValue(ClaimTypes.Name) ??
        this.principal.FindFirstValue("name")!;

    /// <inheritdoc />
    public string FirstName =>
        this.principal.FindFirstValue(ClaimTypes.GivenName) ??
        this.principal.FindFirstValue("given_name")!;

    /// <inheritdoc />
    public string LastName =>
        this.principal.FindFirstValue(ClaimTypes.Surname)
        ?? this.principal.FindFirstValue("family_name")!;

    /// <inheritdoc />
    public string Email =>
        this.principal.FindFirstValue(ClaimTypes.Email) ??
        this.principal.FindFirstValue("email")!;

    /// <inheritdoc />
    public bool IsAdmin =>
        this.principal.IsInRole(this.appSettings.AdminRoleName) ||
        this.principal.Claims.Any(c => c.ValueType == ClaimTypes.Role && c.Value == this.appSettings.AdminRoleName);

    /// <inheritdoc />
    public string FullName =>
        $"{this.FirstName ?? string.Empty} {this.LastName ?? string.Empty}".Trim();

    /// <inheritdoc />
    public bool Claims(Func<Claim, bool> action)
    {
        return this.principal.Claims.Any(action);
    }

    private static ClaimsPrincipal HandleUnauthenticatedUser(IHttpContextAccessor context)
    {
        var authorizationHeader = context.HttpContext?.Request.Headers
            .Where(h => h.Key == "Authorization")
            .Select(h => h.Value)
            .FirstOrDefault()
            .FirstOrDefault();

        if (string.IsNullOrEmpty(authorizationHeader))
        {
            throw new InvalidOperationException(NoUserMessage);
        }

        string token = authorizationHeader.Split(' ', options: StringSplitOptions.RemoveEmptyEntries)[1];
        var jwtToken = new JwtSecurityToken(token);
        return new ClaimsPrincipal(new ClaimsIdentity(jwtToken.Claims));
    }
}
