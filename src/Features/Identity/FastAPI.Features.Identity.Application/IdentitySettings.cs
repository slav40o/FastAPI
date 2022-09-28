namespace FastAPI.Features.Identity.Application;

/// <summary>
/// Identity settings data coming from app settings json and secret stores.
/// </summary>
public sealed class IdentitySettings
{
    /// <summary>
    /// Gets array of user emails that will require admin role on registration.
    /// </summary>
    public string[] AdminEmails { get; init; } = default!;

    /// <summary>
    /// Gets minimal password length required on registration.
    /// </summary>
    public int MinPasswordLength { get; init; }

    /// <summary>
    /// Gets maximum allowed login attempts before user is locked.
    /// </summary>
    public int MaxLoginAttempths { get; init; }

    /// <summary>
    /// Gets a value indicating whether user account should be locked on unsuccessful login.
    /// </summary>
    public bool LockoutUserAccounts { get; init; }

    /// <summary>
    /// Gets lockout time in minutes.
    /// </summary>
    public int LockoutTimeSpanInMinutes { get; init; }

    /// <summary>
    /// Gets a value indicating whether user password will require digit.
    /// </summary>
    public bool RequireDigit { get; init; }

    /// <summary>
    /// Gets a value indicating whether user password will require lower case character.
    /// </summary>
    public bool RequireLowercase { get; init; }

    /// <summary>
    /// Gets a value indicating whether user password will require non alphanumeric characters.
    /// </summary>
    public bool RequireNonAlphanumeric { get; init; }

    /// <summary>
    /// Gets a value indicating whether user password will require upper case character.
    /// </summary>
    public bool RequireUppercase { get; init; }

    /// <summary>
    /// Gets a value indicating whether on user registration authentication token should be generated or not.
    /// </summary>
    public bool LoginOnRegistration { get; init; }

    /// <summary>
    /// Gets access token lifespan in minutes.
    /// </summary>
    public int AuthTokenTimeSpanInMinutes { get; init; }

    /// <summary>
    /// Gets refresh token lifespan in days.
    /// </summary>
    public int RefreshTokenTimeSpanInDays { get; init; }

    /// <summary>
    /// Gets valid issuer URL address. This is the server domain.
    /// </summary>
    public string ValidIssuer { get; init; } = default!;

    /// <summary>
    /// Gets valid audience URL address. This is the client domain.
    /// </summary>
    public string ValidAudience { get; init; } = default!;
}
