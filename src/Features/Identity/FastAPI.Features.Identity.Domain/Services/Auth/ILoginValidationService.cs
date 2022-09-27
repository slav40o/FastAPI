namespace FastAPI.Features.Identity.Domain.Services.Auth;

using FastAPI.Features.Identity.Domain.Entities;

/// <summary>
/// Service responsible for login validation.
/// Handles user lockout.
/// </summary>
public interface ILoginValidationService
{
    /// <summary>
    /// Validates user login by password.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="password">Password for login.</param>
    /// <returns>Whether user login is possible.</returns>
    Task<LoginValidationResult> ValidatePasswordLoginAsync(User user, string password, bool lockoutOnFailure);
}
