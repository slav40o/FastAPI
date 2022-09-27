namespace FastAPI.Features.Identity.Domain.Services.Auth;

using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Features.Identity.Domain.Services.Security;

/// <summary>
/// Login user service.
/// </summary>
public interface ILoginProviderService
{
    /// <summary>
    /// Login given user entity.
    /// </summary>
    /// <param name="user">User entity.</param>
    /// <returns>Generated login data.</returns>
    Task<LoginDataModel> LoginUser(
        User user,
        SecurityTokenOptions authTokenOptions,
        SecurityTokenOptions refreshTokenOptions);
}
