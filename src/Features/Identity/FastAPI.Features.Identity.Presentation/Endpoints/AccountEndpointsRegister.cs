namespace FastAPI.Features.Identity.Presentation.Endpoints;

using FastAPI.Features.Identity.Application.Requests.Account.ChangePassword;
using FastAPI.Features.Identity.Application.Requests.Account.ConfirmEmail;
using FastAPI.Features.Identity.Application.Requests.Account.List;
using FastAPI.Features.Identity.Application.Requests.Account.Register;
using FastAPI.Features.Identity.Application.Requests.Account.UpdateDetails;
using FastAPI.Layers.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;

public sealed class AccountEndpointsRegister : IEndpointRegister
{
    public static string BaseURL => "/api/account";

    public static string ManageURL => "/api/account/manage";

    public void AddEndpoints(WebApplication app)
    {
        app.MediateQueryRequest<ListUsersRequest, UserListItem>(BaseURL);
        app.MediateRequest<RegisterUserRequest, RegisterUserResponseModel>(BaseURL);
        app.MediateRequest<UpdateDetailsRequest>(BaseURL);

        app.MediateRequest<ConfirmEmailRequest>($"{ManageURL}/confirm-email");
        app.MediateRequest<ChangePasswordRequest>($"{ManageURL}/change-password");
    }
}
