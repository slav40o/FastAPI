namespace FastAPI.Features.Identity.Domain.Services.Security;

public sealed class SecurityTokenOptions
{
    public SecurityTokenOptions(int timeSpanInMonutes)
    {
        this.TimeSpanInMinutes = timeSpanInMonutes;
    }

    public SecurityTokenOptions(int timeSpanInMinutes, string? validIssuer, string? validAudience) 
        : this(timeSpanInMinutes)
    {
        ValidIssuer = validIssuer;
        ValidAudience = validAudience;
    }

    /// <summary>
    /// Gets access token lifespan in minutes.
    /// </summary>
    public int TimeSpanInMinutes { get; init; }

    /// <summary>
    /// Gets valid issuer URL address. This is the server domain.
    /// </summary>
    public string? ValidIssuer { get; init; }

    /// <summary>
    /// Gets valid audience URL address. This is the client domain.
    /// </summary>
    public string? ValidAudience { get; init; }
}
