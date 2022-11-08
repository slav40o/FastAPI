namespace FastAPI.Features.Identity.Application.Requests.Account.UpdateDetails;

using FastAPI.Features.Identity.Application.Extensions;
using FastAPI.Features.Identity.Application.Resources;
using FastAPI.Features.Identity.Domain.Repositories;
using FastAPI.Features.Identity.Domain.Services.Users;
using FastAPI.Layers.Application.Handlers;
using FastAPI.Layers.Application.Response;
using FastAPI.Layers.Application.Services;

internal sealed class UpdateDetailsRequestHandler : AppRequestHandler<UpdateDetailsRequest>
{
    private readonly ICurrentUser currentUser;
    private readonly IUserManager userManager;
    private readonly IUserRepository userRepository;

    public UpdateDetailsRequestHandler(
        ICurrentUser currentUser,
        IUserManager userManager,
        IUserRepository userRepository)
    {
        this.currentUser = currentUser;
        this.userManager = userManager;
        this.userRepository = userRepository;
    }

    public override async ValueTask<AppResponse> HandleRequest(UpdateDetailsRequest request, CancellationToken cancellationToken)
    {
        var user = await this.userRepository.FindByIdAsync(request.Id, cancellationToken);
        if (user is null)
        {
            return this.NotFound(UserValidationMessages.UserNotFound);
        }

        if (this.currentUser.UserId is null || (this.currentUser.UserId != user.Id && !this.currentUser.IsAdmin))
        {
            return this.Failure(UserValidationMessages.InvalidUserAccess);
        }

        user.SetFirstName(request.FirstName)
            .SetLastName(request.LastName)
            .SetPhoneNumber(request.PhoneNumber)
            .SetDateOfBirth(request.DateOfBirth)
            .SetGender(request.Gender);

        var identityResult = await this.userManager.UpdateAsync(user);
        if (!identityResult.Succeeded)
        {
            return this.Failure(UserValidationMessages.UpdateDetailsError, identityResult.GetErrors());
        }

        await this.userRepository.SaveAsync(cancellationToken);
        return Success(UserValidationMessages.UpdateDetailsSuccess);
    }
}
