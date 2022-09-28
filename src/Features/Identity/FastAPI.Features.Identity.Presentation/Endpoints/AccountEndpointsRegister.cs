namespace FastAPI.Features.Identity.Presentation.Endpoints;

using FastAPI.Features.Identity.Application.Requests.Users.Register;
using FastAPI.Layers.Presentation.Endpoints;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

public sealed class AccountEndpointsRegister : IEndpointRegister
{
    public static string BaseURL => "/api/account";

    public void AddEndpoints(WebApplication app)
    {
        app.MediateRequest<RegisterUserRequest, RegisterUserResponseModel>(BaseURL);
    }
}
