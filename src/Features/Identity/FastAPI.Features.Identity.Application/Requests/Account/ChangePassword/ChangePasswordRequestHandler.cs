namespace FastAPI.Features.Identity.Application.Requests.Account.ChangePassword;

using FastAPI.Features.Identity.Application.Extensions;
using FastAPI.Features.Identity.Application.Resources;
using FastAPI.Features.Identity.Domain.Repositories;
using FastAPI.Features.Identity.Domain.Services.Users;
using FastAPI.Layers.Application.Handlers;
using FastAPI.Layers.Application.Response;
using FastAPI.Layers.Application.Services;

public sealed class ChangePasswordRequestHandler : AppRequestHandler<ChangePasswordRequest>
{
    private readonly ICurrentUser currentUser;
    private readonly IUserManager userManager;
    private readonly IUserRepository userRepository;

    public ChangePasswordRequestHandler(
        ICurrentUser currentUser,
        IUserManager userManager,
        IUserRepository userRepository)
    {
        this.currentUser = currentUser;
        this.userManager = userManager;
        this.userRepository = userRepository;
    }

    public override async Task<AppResponse> HandleRequest(ChangePasswordRequest request, CancellationToken cancellationToken)
    {
        var user = await this.userRepository.FindByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return AppResponse.NotFound(UserValidationMessages.UserNotFound);
        }

        // TODO: Extract authorization filter/middle-ware or some extension for [CurrentOrAdminAuthorization]
        if (this.currentUser.UserId is null || (this.currentUser.UserId != user.Id && !this.currentUser.IsAdmin))
        {
            return AppResponse.Unauthorized(UserValidationMessages.InvalidUserAccess);
        }

        var identityResult = await this.userManager.ChangePasswordAsync(user, request.OldPassword, request.NewPassword);
        if (!identityResult.Succeeded)
        {
            return AppResponse.ValidationFail(UserValidationMessages.ChangePasswordFail, identityResult.GetErrors());
        }

        return AppResponse.Success(UserValidationMessages.ChangePasswordSuccess);
    }
}