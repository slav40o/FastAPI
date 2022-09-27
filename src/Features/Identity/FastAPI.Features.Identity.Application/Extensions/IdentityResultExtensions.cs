namespace FastAPI.Features.Identity.Application.Extensions;

using FastAPI.Layers.Application.Response;

using Microsoft.AspNetCore.Identity;

/// <summary>
/// Identity result extensions.
/// </summary>
public static class IdentityResultExtensions
{
    /// <summary>
    /// Extract app errors from identity result.
    /// </summary>
    /// <param name="identityResult">Identity result instance.</param>
    /// <returns>App errors array.</returns>
    public static AppError[] GetErrors(this IdentityResult identityResult)
        => identityResult.Errors.Select(e => new AppError(e.Code, e.Description)).ToArray();
}
