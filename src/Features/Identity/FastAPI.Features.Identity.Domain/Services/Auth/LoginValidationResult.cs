namespace FastAPI.Features.Identity.Domain.Services.Auth;

/// <summary>
/// Validation result data.
/// </summary>
public record LoginValidationResult
{
    private readonly DateTimeOffset? lockPeriodEnd;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginValidationResult"/> class.
    /// </summary>
    /// <param name="success">Login success.</param>
    /// <param name="isLocked">Account locked status.</param>
    /// <param name="lockPeriodEnd">Date time offset with showing at what point of time user will be able try login again.</param>
    public LoginValidationResult(bool success, bool isLocked, DateTimeOffset? lockPeriodEnd)
    {
        this.Success = success;
        this.IsAccountLocked = isLocked;

        this.lockPeriodEnd = lockPeriodEnd;
    }

    /// <summary>
    /// Gets a value indicating whether login attempt is successful.
    /// </summary>
    public bool Success { get; init; }

    /// <summary>
    /// Gets a value indicating whether after failed login attempt user account is locked.
    /// </summary>
    public bool IsAccountLocked { get; init; }

    /// <summary>
    /// Gets value for the account lock period in minutes.
    /// </summary>
    public int LockedPeriodInMinutes
        => this.lockPeriodEnd is null ? 0 : (this.lockPeriodEnd.Value.UtcDateTime - DateTime.UtcNow).Minutes;
}
