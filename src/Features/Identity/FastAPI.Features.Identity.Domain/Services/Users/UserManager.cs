namespace FastAPI.Features.Identity.Domain.Services.Users;

using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Features.Identity.Domain.Events;

/// <inheritdoc />
public sealed class UserManager : IUserManager
{
    private readonly UserManager<User> manager;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserManager"/> class.
    /// </summary>
    /// <param name="manager">Identity user manager.</param>
    public UserManager(UserManager<User> manager)
    {
        this.manager = manager;
    }

    /// <inheritdoc />
    public Task<IdentityResult> AddToRoleAsync(User user, string role)
        => manager.AddToRoleAsync(user, role);

    /// <inheritdoc />
    public Task<IdentityResult> AddLoginAsync(User user, UserLoginInfo info)
        => manager.AddLoginAsync(user, info);

    /// <inheritdoc />
    public Task<IdentityResult> ChangePasswordAsync(User user, string password, string newPassword)
        => manager.ChangePasswordAsync(user, password, newPassword);

    /// <inheritdoc />
    public Task<string> GenerateResetPasswordTokenAsync(User user)
        => manager.GeneratePasswordResetTokenAsync(user);

    /// <inheritdoc />
    public Task<IdentityResult> ResetPasswordAsync(User user, string token, string newPassword)
        => manager.ResetPasswordAsync(user, token, newPassword);

    /// <inheritdoc />
    public Task<IdentityResult> ConfirmEmailAsync(User user, string? token)
        => manager.ConfirmEmailAsync(user, token);

    /// <inheritdoc />
    public async Task<IdentityResult> CreateAsync(User user, string password)
    {
        var result = await manager.CreateAsync(user, password);
        if (result.Succeeded)
        {
            user.AddEvent(new UserCreatedEvent(user));
        }

        return result;
    }

    /// <inheritdoc />
    public Task DeleteAsync(User user)
        => manager.DeleteAsync(user);

    /// <inheritdoc />
    public Task<string?> GenerateEmailConfirmationTokenAsync(User user)
        => manager.GenerateEmailConfirmationTokenAsync(user);

    /// <inheritdoc />
    public Task<IList<Claim>> GetClaimsAsync(User user)
        => manager.GetClaimsAsync(user);

    /// <inheritdoc />
    public Task<IList<string>> GetRolesAsync(User user)
        => manager.GetRolesAsync(user);

    /// <inheritdoc />
    public Task SetEmailAsync(User user, string email)
        => manager.SetEmailAsync(user, email);

    /// <inheritdoc />
    public Task<IdentityResult> UpdateAsync(User user)
        => manager.UpdateAsync(user);
}
