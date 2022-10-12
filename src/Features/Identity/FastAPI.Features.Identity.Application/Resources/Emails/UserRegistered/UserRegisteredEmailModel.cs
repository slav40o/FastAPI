namespace FastAPI.Features.Identity.Application.Resources.Emails.UserRegistered;

/// <summary>
/// User registered email model.
/// </summary>
public sealed class UserRegisteredEmailModel
{
    /// <summary>
    /// Gets new user Id.
    /// </summary>
    public string Id { get; init; } = default!;

    /// <summary>
    /// Gets new user name.
    /// </summary>
    public string Name { get; init; } = default!;

    /// <summary>
    /// Gets new user email confirmation token.
    /// </summary>
    public string ConfirmationToken { get; init; } = default!;

    /// <summary>
    /// Gets confirmation endpoint URL.
    /// </summary>
    public string ConfirmationUrl { get; init; } = default!;
}