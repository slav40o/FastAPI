namespace FastAPI.Features.Identity.Presentation.Endpoints;

using FastAPI.Features.Identity.Application.Requests.Users.List;
using FastAPI.Features.Identity.Application.Requests.Users.Register;
using FastAPI.Features.Identity.Application.Requests.Users.UpdateDetails;
using FastAPI.Layers.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;

public sealed class AccountEndpointsRegister : IEndpointRegister
{
    public static string BaseURL => "/api/account";

    public void AddEndpoints(WebApplication app)
    {
        app.MediateQueryRequest<ListUsersRequest, UserListItem>(BaseURL);
        app.MediateRequest<RegisterUserRequest, RegisterUserResponseModel>(BaseURL);
        app.MediateRequest<UpdateDetailsRequest>(BaseURL);
    }
}
