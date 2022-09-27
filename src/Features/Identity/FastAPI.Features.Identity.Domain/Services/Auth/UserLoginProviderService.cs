namespace FastAPI.Features.Identity.Domain.Services.Auth;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Features.Identity.Domain.Services.Security;
using FastAPI.Features.Identity.Domain.Services.Users;

/// <inheritdoc/>
internal class UserLoginProviderService : ILoginProviderService
{
    private readonly IUserManager userManager;
    private readonly IAuthTokenProvider tokenService;

    /// <summary>
    /// Initializes a new instance of the <see cref="UserLoginProviderService"/> class.
    /// </summary>
    /// <param name="userManager">User manager service.</param>
    /// <param name="tokenService">Authentication tokens service.</param>
    public UserLoginProviderService(IUserManager userManager, IAuthTokenProvider tokenService)
    {
        this.userManager = userManager;
        this.tokenService = tokenService;
    }

    /// <inheritdoc />
    public async Task<LoginDataModel> LoginUser(
        User user,
        SecurityTokenOptions authTokenOptions,
        SecurityTokenOptions refreshTokenOptions)
    {
        var roles = await this.userManager.GetRolesAsync(user);
        var claims = await this.userManager.GetClaimsAsync(user);
        var authToken = this.tokenService.GenerateAuthToken(user, roles, claims, authTokenOptions);
        var refreshToken = this.tokenService.GenerateRefreshToken(refreshTokenOptions);

        user.SetNewRefreshToken(new IdentityToken(refreshToken.Value, refreshToken.ExpirationTime));
        await this.userManager.UpdateAsync(user);

        return new LoginDataModel(authToken, refreshToken);
    }
}
