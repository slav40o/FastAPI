namespace FastAPI.Features.Identity.Domain.Services.Users;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FastAPI.Features.Identity.Domain.Entities;

/// <summary>
/// User manager wrapper interface.
/// </summary>
public interface IUserManager
{
    /// <summary>
    /// Create new user.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="password">User password.</param>
    /// <returns>Task holding identity result.</returns>
    Task<IdentityResult> CreateAsync(User user, string password);

    /// <summary>
    /// Set user email.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="email">New user email.</param>
    /// <returns>Performed task.</returns>
    Task SetEmailAsync(User user, string email);

    /// <summary>
    /// Delete user.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <returns>Performed task.</returns>
    Task DeleteAsync(User user);

    /// <summary>
    /// Generate new email confirmation token.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <returns>Task holding generated token.</returns>
    Task<string?> GenerateEmailConfirmationTokenAsync(User user);

    /// <summary>
    /// Add user to given role.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="role">Role.</param>
    /// <returns>Task holding identity result.</returns>
    Task<IdentityResult> AddToRoleAsync(User user, string role);

    /// <summary>
    /// Add login information for the given user.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="info">Login information.</param>
    /// <returns>Task holding identity result.</returns>
    Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo info);

    /// <summary>
    /// Confirm user email address.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="token">Email confirmation token.</param>
    /// <returns>Task holding identity result.</returns>
    Task<IdentityResult> ConfirmEmailAsync(User user, string? token);

    /// <summary>
    /// Get list of roles assigned to given user.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <returns>List with user roles.</returns>
    Task<IList<string>> GetRolesAsync(User user);

    /// <summary>
    /// Get list of claims assigned to given user.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <returns>List with user claims.</returns>
    Task<IList<Claim>> GetClaimsAsync(User user);

    /// <summary>
    /// Change user password.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="password">User old password.</param>
    /// <param name="newPassword">user new password.</param>
    /// <returns>Task holding identity result.</returns>
    Task<IdentityResult> ChangePasswordAsync(User user, string password, string newPassword);

    /// <summary>
    /// Generate reset password token.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <returns>Task holding identity result.</returns>
    Task<string> GenerateResetPasswordTokenAsync(User user);

    /// <summary>
    /// Reset user password.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <param name="token">Reset password token.</param>
    /// <param name="newPassword">New user password.</param>
    /// <returns>Task holding identity result.</returns>
    Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword);

    /// <summary>
    /// Save updated user details.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <returns>Task holding identity result.</returns>
    Task<IdentityResult> UpdateAsync(User user);
}
