namespace FastAPI.Features.Identity.Application.Services;

using FastAPI.Features.Identity.Domain.Entities;

/// <summary>
/// Identity email service.
/// </summary>
public interface IIdentityEmailService
{
    /// <summary>
    /// Send user registered mail.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="token">User confirmation token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Performed task.</returns>
    Task SendUserRegisteredEmail(User user, string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send user email confirmation mail.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="token">User confirmation token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Performed task.</returns>
    Task SendUserEmailConfirmationEmail(User user, string token, CancellationToken cancellationToken = default);

    /// <summary>
    /// Send user password reset mail.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="token">User reset token.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>Performed task.</returns>
    Task SendUserPasswordResetEmail(User user, string token, CancellationToken cancellationToken = default);
}
