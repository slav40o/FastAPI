namespace FastAPI.Features.Identity.Application.Requests.Account.ConfirmEmail;

using FastAPI.Features.Identity.Application.Extensions;
using FastAPI.Features.Identity.Application.Resources;
using FastAPI.Features.Identity.Domain.Repositories;
using FastAPI.Features.Identity.Domain.Services.Users;
using FastAPI.Layers.Application;
using FastAPI.Layers.Application.Handlers;
using FastAPI.Layers.Application.Response;
using FastAPI.Layers.Application.Services;

internal sealed class ConfirmEmailRequestHandler : AppRequestHandler<ConfirmEmailRequest>
{
    private readonly ICurrentUser currentUser;
    private readonly IUserManager userManager;
    private readonly IUserRepository userRepository;
    private readonly IHttpUtilities httpUtilities;

    public ConfirmEmailRequestHandler(
        ICurrentUser currentUser,
        IUserManager userManager,
        IUserRepository userRepository,
        IHttpUtilities httpUtilities)
    {
        this.currentUser = currentUser;
        this.userManager = userManager;
        this.userRepository = userRepository;
        this.httpUtilities = httpUtilities;
    }

    public override async ValueTask<AppResponse> HandleRequest(ConfirmEmailRequest request, CancellationToken cancellationToken)
    {
        var user = await this.userRepository.FindByIdAsync(request.UserId, cancellationToken);
        if (user is null)
        {
            return AppResponse.NotFound(UserValidationMessages.UserNotFound);
        }

        if (user.Id != this.currentUser.UserId)
        {
            return AppResponse.NotFound(UserValidationMessages.InvalidUserAccess);
        }

        string? token = this.httpUtilities.UrlDecode(request.Token);
        var identityResult = await this.userManager.ConfirmEmailAsync(user, token);

        if (!identityResult.Succeeded)
        {
            return AppResponse.ValidationFail(UserValidationMessages.ConfirmEmailFail, identityResult.GetErrors());
        }

        return AppResponse.Success(UserValidationMessages.ConfirmEmailSuccess);
    }
}