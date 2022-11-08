namespace FastAPI.Features.Identity.Application.Requests.Account.Register;

using FastAPI.Features.Identity.Application.Extensions;
using FastAPI.Features.Identity.Application.Resources;
using FastAPI.Features.Identity.Domain.Builders.Abstractions;
using FastAPI.Features.Identity.Domain.Entities;
using FastAPI.Features.Identity.Domain.Services.Auth;
using FastAPI.Features.Identity.Domain.Services.Security;
using FastAPI.Features.Identity.Domain.Services.Users;
using FastAPI.Layers.Application.Handlers;
using FastAPI.Layers.Application.Response;
using FastAPI.Layers.Application.Settings;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

internal sealed class RegisterUserRequestHandler : AppRequestHandler<RegisterUserRequest, RegisterUserResponseModel>
{
    private readonly AppSettings appSettings;
    private readonly IdentitySettings identitySettings;

    private readonly IUserManager userManager;
    private readonly IUserBuilder userBuilder;
    private readonly ILoginProviderService loginService;

    public RegisterUserRequestHandler(
        IOptions<AppSettings> appSettings,
        IOptions<IdentitySettings> identitySettings,
        IUserManager userManager,
        IUserBuilder userBuilder,
        ILoginProviderService loginService)
    {
        this.appSettings = appSettings.Value;
        this.identitySettings = identitySettings.Value;
        this.userManager = userManager;
        this.userBuilder = userBuilder;
        this.loginService = loginService;
    }

    public override async ValueTask<AppResponse<RegisterUserResponseModel>> HandleRequest(RegisterUserRequest request, CancellationToken cancellationToken)
    {
        var user = this.userBuilder
            .WithEmail(request.Email)
            .WithFirstName(request.FirstName)
            .WithLastName(request.LastName)
            .Build();

        var identityResult = await this.userManager.CreateAsync(user, request.Password);
        if (!identityResult.Succeeded)
        {
            return this.Failure(UserValidationMessages.RegistrationFailed, identityResult.GetErrors());
        }

        var setRolesResult = await this.SetAdminRole(request, user, identityResult);
        if (!setRolesResult.Succeeded)
        {
            // Revert user creation on identity error.
            await this.userManager.DeleteAsync(user);
            return this.Failure(UserValidationMessages.RegistrationFailed);
        }

        LoginDataModel? loginData = null;
        if (this.identitySettings.LoginOnRegistration)
        {
            var authTokenOptions = new SecurityTokenOptions(
                this.identitySettings.AuthTokenTimeSpanInMinutes,
                this.identitySettings.ValidIssuer,
                this.identitySettings.ValidAudience);

            var refreshTokenOptions = new SecurityTokenOptions(this.identitySettings.RefreshTokenTimeSpanInDays * 24 * 60);
            loginData = await this.loginService.LoginUser(user, authTokenOptions, refreshTokenOptions);
        }

        return this.Success(UserValidationMessages.RegistrationSucceeded, new RegisterUserResponseModel(user.Id, loginData));
    }

    private async Task<IdentityResult> SetAdminRole(RegisterUserRequest request, User user, IdentityResult identityResult)
    {
        if (identityResult.Succeeded &&
            this.identitySettings.AdminEmails is not null &&
            this.identitySettings.AdminEmails.Contains(request.Email))
        {
            identityResult = await this.userManager.AddToRoleAsync(user, this.appSettings.AdminRoleName);
        }

        return identityResult;
    }
}
