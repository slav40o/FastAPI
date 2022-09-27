namespace FastAPI.Features.Identity.Domain.Services.Auth;

using FastAPI.Features.Identity.Domain.Entities;

using Microsoft.AspNetCore.Identity;

using System.Threading.Tasks;

/// <inheritdoc/>
internal class LoginValidationService : ILoginValidationService
{
    private readonly SignInManager<User> signInManager;

    /// <summary>
    /// Initializes a new instance of the <see cref="LoginValidationService"/> class.
    /// </summary>
    /// <param name="signInManager">Identity sign-in manager.</param>
    /// <param name="identitySettingsOptions">Identity settings.</param>
    public LoginValidationService(SignInManager<User> signInManager)
    {
        this.signInManager = signInManager;
    }

    /// <inheritdoc/>
    public async Task<LoginValidationResult> ValidatePasswordLoginAsync(User user, string password, bool lockoutOnFailure)
    {
        var signInResult = await signInManager.PasswordSignInAsync(
            user: user,
            password: password,
            isPersistent: false,
            lockoutOnFailure: lockoutOnFailure);

        return new LoginValidationResult(
            signInResult.Succeeded,
            signInResult.IsLockedOut,
            user.LockoutEnd);
    }
}
